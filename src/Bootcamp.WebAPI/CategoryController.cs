using Bootcamp.Application.Category.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Bootcamp.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>
        
        [HttpGet]
        [Route("fetch/all")]
        public async Task<ActionResult> GetCategoriesAll()
        {
            return null;
        }

        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateCategories(CategoryRequestDto request)
        {
            return null;
        }

        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpPost]
        [Route("edit")]
        public async Task<ActionResult> UpdateCategories()
        {
            return null;
        }
        /// <summary>
        /// Get list of categories with pagination and can be searched
        /// by category name too
        /// </summary>

        [HttpGet]
        [Route("fetch")]
        public async Task<ActionResult> GetCategoriesByid()
        {
            return null;
        }


    }
}
