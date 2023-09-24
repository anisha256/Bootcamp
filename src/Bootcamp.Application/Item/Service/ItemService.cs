using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Models;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Item.Interface;
using Bootcamp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Application.Item.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<GenericAPIResponse<string>> AddItem(ItemRequestDto request)
        {
            var response = new GenericAPIResponse<string>();
            var cancellationToken = new CancellationToken();
            response.Success = false;
            try
            {
                var item = new Domain.Entities.Item();
                item.Name = request.Name;
                item.Description = request.Description ?? null;
                item.Quantity = request.Quantity;
                item.Price = request.Price;
                item.ThresholdQuantity = request.ThresholdQuantity;
                item.IsAvailable = request.IsAvailable;
                item.CreatedOn = DateTime.UtcNow;
                Guid itemId = await _unitOfWork.GenericRepository<Domain.Entities.Item>().InsertAndGetIdAsync(item);

                if (itemId == Guid.Empty)
                {
                    response.Message = "Item is not created!";

                }
                var categoryItem = new CategoryItem();
                categoryItem.ItemId = itemId;
                categoryItem.CategoryId = request.CategoryId;
                categoryItem.CreatedOn = DateTime.UtcNow;
                await _unitOfWork.GenericRepository<CategoryItem>().InsertAsync(categoryItem);

                await _unitOfWork.CommitAsync(cancellationToken);

                response.Success = true;
                response.Message = "Item is created successfully!!";
                response.Data = itemId.ToString();


            }
            catch (OperationCanceledException)
            {
                response.Message = "Operation was canceled.";
            }
            catch (Exception ex)
            {
                response.Message = "Failed to create Item ," + ex.Message;
            }
            return response;
        }


        public async Task<GenericAPIResponse<string>> UpdateItem(ItemRequestDto request, Guid id)
        {
            var response = new GenericAPIResponse<string>();
            var cancellationToken = new CancellationToken();
            response.Success = false;

            try
            {
                var item = await _unitOfWork.GenericRepository<Domain.Entities.Item>().GetByIdAsync(id);

                if (item == null)
                {
                    response.Message = "Item not found";
                }
                else
                {
                    item.Name = request.Name;
                    item.Description = request.Description ?? null;
                    item.Quantity = request.Quantity;
                    item.Price = request.Price;
                    item.ThresholdQuantity = request.ThresholdQuantity;
                    item.IsAvailable = request.IsAvailable;

                    item.CreatedOn = DateTime.UtcNow;

                    _unitOfWork.GenericRepository<Domain.Entities.Item>().Update(item);

                    var categoryItem = await _unitOfWork.GenericRepository<CategoryItem>()
                        .GetAllAsync()
                        .Result
                        .Where(x => x.ItemId == id)
                        .FirstOrDefaultAsync();

                    if (categoryItem == null)
                    {
                        response.Message = "Category item not found";
                    }
                    else
                    {

                        categoryItem.CategoryId = request.CategoryId;
                        categoryItem.ModifiedOn = DateTime.UtcNow;
                        _unitOfWork.GenericRepository<CategoryItem>().Update(categoryItem);

                        await _unitOfWork.CommitAsync(cancellationToken);

                        response.Success = true;
                        response.Message = "Item updated successfully";
                        response.Data = id.ToString();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                response.Message = "Operation was canceled.";
            }
            catch (Exception ex)
            {
                response.Message = "Failed to update Item: " + ex.Message;
            }

            return response;
        }


        public async Task<GenericAPIResponse<string>> DeleteItem(Guid id)
        {
            var response = new GenericAPIResponse<string>();
            var cancellationToken = new CancellationToken();
            response.Success = false;
            try
            {
                var item = await _unitOfWork.GenericRepository<Domain.Entities.Item>().GetByIdAsync(id);

                if (item == null)
                {
                    response.Message = "Item not found";
                }
                else
                {
                    item.DeleteFlag = true;
                    item.DeletedOn = DateTime.UtcNow;
                    _unitOfWork.GenericRepository<Domain.Entities.Item>().Update(item);
                    var categoryItem = await _unitOfWork.GenericRepository<CategoryItem>()
                      .GetAllAsync()
                      .Result
                      .Where(x => x.ItemId == id)
                      .FirstOrDefaultAsync();

                    if (categoryItem == null)
                    {
                        response.Message = "Category item not found";
                    }
                    else
                    {
                        categoryItem.DeleteFlag = true;
                        categoryItem.DeletedOn = DateTime.UtcNow;

                        _unitOfWork.GenericRepository<CategoryItem>().Update(categoryItem);
                        await _unitOfWork.CommitAsync(cancellationToken);

                        response.Success = true;
                        response.Message = "Item deleted successfully";
                    }
                }

            }
            catch (OperationCanceledException)
            {
                response.Message = "Operation was canceled.";
            }
            catch (Exception ex)
            {
                response.Message = "Failed to delete Item: " + ex.Message;
            }

            return response;
        }
    }
}
