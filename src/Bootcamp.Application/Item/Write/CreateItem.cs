using Bootcamp.Application.Interfaces;
using Bootcamp.Application.Interfaces.Services;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Models;
using Bootcamp.Domain.Entities;

namespace Bootcamp.Application.Item.Write
{
    public class CreateItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        public CreateItem(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;

        }

        public async Task<GenericAPIResponse<string>> AddItem(ItemRequestDto model)
        {
            var response = new GenericAPIResponse<string>();
            response.Success = false;
            try
            {
                var item = new Bootcamp.Domain.Entities.Item();
                item.Name = model.Name;
                item.Description = model.Description ?? null;
                item.Quantity = model.Quantity;
                item.Price = model.Price;
                item.ThresholdQuantity = model.ThresholdQuantity;
                item.IsAvailable = model.IsAvailable;
                item.CreatedOn = DateTime.UtcNow;
                Guid itemId = await _unitOfWork.GenericRepository<Bootcamp.Domain.Entities.Item>().InsertAndGetIdAsync(item);
                if (itemId == Guid.Empty)
                {
                    response.Message = "Item is not created!";

                }
                var categoryItem = new CategoryItem();
                categoryItem.ItemId = model.CategoryId;
                categoryItem.CreatedOn = DateTime.UtcNow;



            }
            catch (Exception ex)
            {
                response.Message = "Failed to create Item" + ex.Message;
            }


            return response;




        }

    }
}
