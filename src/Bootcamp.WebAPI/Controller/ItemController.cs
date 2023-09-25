using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
       
     
      /*  [HttpPost]
        public async Task<GenericAPIResponse<string>> Create([FromBody]CreateItemCommand command)
        {
            var response = new GenericAPIResponse<string>();
            //var res = await _itemService.AddItem(request);
           *//* if(res != null)
            {
                response.Success = res.Success;
                response.Message = res.Message;
            }*//*
            return response;
        }
   

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
        }*/
    }
}
