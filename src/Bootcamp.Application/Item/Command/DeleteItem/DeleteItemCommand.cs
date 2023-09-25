using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Models;
using Bootcamp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Command.DeleteItem
{
    public class DeleteItemCommand : IRequest<GenericAPIResponse<string>>
    {
        public Guid id { get; set; }

    };

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, GenericAPIResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericAPIResponse<string>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
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
                    item.DeleteFlag = true;
                    item.DeletedOn = DateTime.UtcNow;
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
