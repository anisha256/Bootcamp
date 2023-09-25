using Bootcamp.Application.Common.Models;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Item.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;

        }
        /// <summary>
        /// Api to create a Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericAPIResponse<string>> Create([FromBody]ItemRequestDto model)
        {
            var response = new GenericAPIResponse<string>();
            var res = await _itemService.AddItem(model);
            if(res != null)
            {
                response.Success = res.Success;
                response.Message = res.Message;
            }
            return response;
        }
        /// <summary>
        /// api to update the item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<GenericAPIResponse<string>> Update(Guid id, [FromBody]ItemRequestDto request)
        {
            var response = new GenericAPIResponse<string>();
            var res = await _itemService.UpdateItem(request,id);
            if (res != null)
            {
                response.Success = res.Success;
                response.Message = res.Message;
            }
            return response;
        }

        /// <summary>
        /// api to delete the item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<GenericAPIResponse<string>> Delete(Guid id)
        {
            var response = new GenericAPIResponse<string>();
            var res = await _itemService.DeleteItem(id);
            if (res != null)
            {
                response.Success = res.Success;
                response.Message = res.Message;
            }
            return response;
        }
    }
}
