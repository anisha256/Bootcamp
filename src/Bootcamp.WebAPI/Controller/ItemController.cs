using Bootcamp.Application.Common.Models;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Item.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.WebAPI.Controller
{
    [Route("api/[controller]")]
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
        [Route("create")]
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

        [HttpPut]
        [Route("update")]
        public async Task<GenericAPIResponse<string>> Update([FromBody]UpdateItemDto request)
        {
            var response = new GenericAPIResponse<string>();
            var res = await _itemService.UpdateItem(request);
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
        [HttpPut]
        [Route("delete")]
        public async Task<GenericAPIResponse<string>> Delete([FromQuery]Guid id)
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

        /// <summary>
        /// api to fetch item details by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("fetch-by-id")]
        public async Task<ActionResult<ItemResponseDto>> GetItemDetailsById([FromQuery]Guid id)
        {
            return  await _itemService.GetItemById(id);
        }

        [HttpGet]
        [Route("fetch-all")]
        public async Task<ActionResult<List<ItemResponseDto>>> GetAllItems()
        {
            return await _itemService.GetAllItems();
        }
    }
}
