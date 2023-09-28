
using Bootcamp.Application.Category.Dto;
using Bootcamp.Application.Common.Models;


namespace Bootcamp.Application.Category.Interface
{
    public interface ICategoryservice
    {
        Task<GenericAPIResponse<string>> CreateCategory( CategoryRequestDto request);
        Task<List<CategoryDto>> GetAllCategory();
        Task<GenericAPIResponse<string>> UpdateCategory( CategoryDto request);
        Task<CategoryDto> GetCategoryById(Guid id);
        Task<GenericAPIResponse<string>> DeleteCategoryById(Guid id);
        Task<List<CategoryResponseDto>> GetCategoriesItems();
    }
}
