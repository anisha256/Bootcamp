using Bootcamp.Application.Category.DTO;
using Bootcamp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Category.Interface
{
    public interface ICategoryservice
    {
        Task<GenericAPIResponse<string>> CreateCategory( CategoryRequestDto request);
        Task<List<CategoryDto>> GetAllCategory();
        Task<GenericAPIResponse<string>> UpdateCategory( CategoryDto request);
        Task<CategoryDto> GetCategoryById(Guid id);
        Task<GenericAPIResponse<string>> DeleteCategoryById(Guid id);
    }
}
