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
        public async Task<GenericAPIResponse<string>> Create([FromBody]ItemRequestDto model,CancellationToken cancellationToken)
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

    /*    [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            return NoContent();
        }*/

        [HttpDelete("{id}")]
        [Route("delete")]
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
