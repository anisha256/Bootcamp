using Bootcamp.Application.Category.Dto;
using Bootcamp.Application.Category.Service;
using Bootcamp.Application.Category.Interface;
using Bootcamp.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bootcamp.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       
        private readonly ICategoryservice categoryService;
        public CategoryController(ICategoryservice categoryservice)
        {
            this.categoryService = categoryservice;
        }
        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>
        /// 
        [HttpGet]
        [Route("fetch/all")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoriesAll()
        {
            var result= await categoryService.GetAllCategory();
            return result ;
        }

        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericAPIResponse<string>>> CreateCategories( CategoryRequestDto request, CancellationToken cancellationToken)
        {
            //var reponse = new GenericAPIResponse<string>();
            var result =  await categoryService.CreateCategory(request);
            return result;


        }

        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpPut]
        [Route("edit")]
        public async Task<ActionResult<GenericAPIResponse<string>>> UpdateCategories( CategoryDto request)
        {
             var result = await categoryService.UpdateCategory(request);
            return result;
        }
        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpGet]
        [Route("categorybyid")]
        public async Task<ActionResult<CategoryDto>> GetCategoriesByid( Guid id)
        {
            var result = await categoryService.GetCategoryById(id);
            return result;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<GenericAPIResponse<string>>> Delete(Guid id)
        {
            var result = await categoryService.DeleteCategoryById(id);
            return result;
        }

        [HttpGet]
        [Route("fetch-all-categories/items")]
        public async Task<ActionResult<CategoryRequestDto>> GetCategoriesItem()
        {
            return await categoryService.GetCategoriesItems();
        }

    }
}
