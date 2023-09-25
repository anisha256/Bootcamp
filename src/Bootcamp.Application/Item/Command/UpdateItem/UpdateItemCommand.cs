using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Models;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Command.UpdateItem
{
    public class UpdateItemCommand : ItemRequestDto, IRequest<GenericAPIResponse<string>>
    {
        public Guid id { get; set; }
        public UpdateItemCommand(ItemRequestDto itemRequestDto)
        {

        }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand,GenericAPIResponse<string>>
    {
        public readonly IUnitOfWork _unitOfWork;
        public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
        {
            
        _unitOfWork = unitOfWork;
        }

        public async Task<GenericAPIResponse<string>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericAPIResponse<string>();
            response.Success = false;

            try
            {
                var item = await _unitOfWork.GenericRepository<Domain.Entities.Item>().GetByIdAsync(request.id);

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
                        .Where(x => x.ItemId == request.id)
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
    }
}
