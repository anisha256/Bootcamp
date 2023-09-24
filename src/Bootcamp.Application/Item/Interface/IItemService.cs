using Bootcamp.Application.Common.Models;
using Bootcamp.Application.Item.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Interface
{
    public interface IItemService
    {
        Task<GenericAPIResponse<string>> AddItem(ItemRequestDto request);
        Task<GenericAPIResponse<string>> UpdateItem(ItemRequestDto request, Guid id);
       Task<GenericAPIResponse<string>> DeleteItem(Guid id);


    }
}
