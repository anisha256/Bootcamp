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
                var itemAlreadyExists = _unitOfWork.GenericRepository<Domain.Entities.Item>().GetAllAsync().Result.Any(x => x.Name.ToUpper() == request.Name.ToUpper());
                if (itemAlreadyExists)
                {
                    response.Message = "Item name already exists";
                    return response;
                }
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
                    return response;

                }
                if (request.Categories == null)
                {
                    response.Message = "At least 1 category should be selected.";
                    return response;
                }

                foreach (var category in request.Categories)
                {
                    var categoryItem = new CategoryItem();
                    categoryItem.ItemId = itemId;
                    categoryItem.CategoryId = category;
                    categoryItem.CreatedOn = DateTime.UtcNow;
                    await _unitOfWork.GenericRepository<CategoryItem>().InsertAsync(categoryItem);
                }
                await _unitOfWork.CommitAsync(cancellationToken);

                response.Success = true;
                response.Message = "Item is created successfully!!";
                response.Data = itemId.ToString();


            }
            catch (Exception ex)
            {
                response.Message = "Failed to create Item ," + ex.Message;
            }
            return response;
        }


        public async Task<GenericAPIResponse<string>> UpdateItem(UpdateItemDto request)
        {
            var response = new GenericAPIResponse<string>();
            var cancellationToken = new CancellationToken();
            response.Success = false;

            try
            {
                var item = await _unitOfWork.GenericRepository<Domain.Entities.Item>().GetByIdAsync(request.Id);
                if (item == null)
                {
                    response.Message = "Item not found!!";
                }
                var itemCategories = _unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result.Where(category => request.Categories.Contains(category.Id)).ToList();

                if (itemCategories.Count() != request.Categories.Count())
                {
                    response.Message = "Item Categories not found";
                }

                if (request.Name != null && request.Name.ToUpper() != item.Name.ToUpper())
                {
                    var alreadyExists = _unitOfWork.GenericRepository<Domain.Entities.Item>().GetAllAsync().Result.Where(x => x.Name == request.Name).Any();
                    if (alreadyExists)
                    {
                        response.Message = "Item Name already exists.";
                    }
                    item.Name = request.Name;
                }

                item.Description = request.Description ?? null;
                item.Quantity = request.Quantity;
                item.Price = request.Price;
                item.ThresholdQuantity = request.ThresholdQuantity;
                item.IsAvailable = request.IsAvailable;
                item.ImageUrl = request.ImageUrl;

                var actualCategoriesOfItem = await _unitOfWork.GenericRepository<Domain.Entities.CategoryItem>().GetAllAsync().Result.Where(x => x.ItemId == request.Id).ToListAsync();
                if (actualCategoriesOfItem == null)
                {
                    response.Message = "Failed to fetch item categories.";
                    return response;
                }

                var categoryToAdd = itemCategories.Where(i => !actualCategoriesOfItem.Where(a => a.CategoryId == i.Id).Any()).ToList();
                var categoryToRemove = actualCategoriesOfItem.Where(x => !itemCategories.Where(i => i.Id == x.CategoryId).Any()).ToList();

                foreach (var category in categoryToAdd)
                {
                    var newItemCategory = new CategoryItem()
                    {
                        CategoryId = category.Id,
                        ItemId = item.Id,
                        ModifiedOn = DateTime.UtcNow,
                    };
                    await _unitOfWork.GenericRepository<CategoryItem>().InsertAsync(newItemCategory);

                }

                foreach (var category in categoryToRemove)
                {

                    await _unitOfWork.GenericRepository<CategoryItem>().DeleteAsync(category);

                }

                _unitOfWork.GenericRepository<Domain.Entities.Item>().Update(item);



                await _unitOfWork.CommitAsync(cancellationToken);

                response.Success = true;
                response.Message = "Item updated successfully";


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
                categoryItem.DeleteFlag = true;
                categoryItem.DeletedOn = DateTime.UtcNow;

                _unitOfWork.GenericRepository<CategoryItem>().Update(categoryItem);
                await _unitOfWork.CommitAsync(cancellationToken);

                response.Success = true;
                response.Message = "Item deleted successfully";


            }

            catch (Exception ex)
            {
                response.Message = "Failed to delete Item: " + ex.Message;
            }

            return response;
        }

        public Task<ItemResponseDto> GetItemById(Guid id)
        {
            try
            {
                var categoryItems = _unitOfWork.GenericRepository<CategoryItem>().GetAllAsync().Result.ToList();
                var categoryDetails = _unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result;

                var getCategoriesOfItem = (from c in categoryItems
                                           join cd in categoryDetails
                                           on c.CategoryId equals cd.Id
                                           where c.ItemId == id
                                           && c.DeleteFlag != true
                                           select new ItemCategories
                                           {
                                               CategoryId = cd.Id,
                                               CategoryName = cd.Name
                                           }).ToList();

                var item = _unitOfWork.GenericRepository<Domain.Entities.Item>().GetAllAsync().Result.FirstOrDefault(x => x.Id == id);
                if (item == null)
                {
                    throw new Exception("Item not found");
                }

                var response = new ItemResponseDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    Quantity = item.Quantity,
                    ThresholdQuantity = item.ThresholdQuantity,
                    IsAvailable = item.IsAvailable,
                    CreatedOn = item.CreatedOn,
                    DeleteFlag = item.DeleteFlag,
                    ItemCategories = getCategoriesOfItem ?? null

                };

                return Task.FromResult(response);

            }
            catch (Exception ex)
            {
                throw new Exception("Cannot fetch the item ", ex);
            }
        }
    }
}
