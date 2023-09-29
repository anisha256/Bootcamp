using Bootcamp.Application.Category.Dto;
using Bootcamp.Application.Category.Interface;
using Bootcamp.Application.Common.Exceptions;
using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Application.Category.Service
{
    public class CategoryService : ICategoryservice
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public async Task<GenericAPIResponse<string>> CreateCategory(CategoryRequestDto request)
        {
            var cancellationToken = new CancellationToken();
            var reponse = new GenericAPIResponse<string>();
            try
            {
                var entity = new Domain.Entities.Category
                {
                    Name = request.Name,
                    CreatedOn = DateTime.UtcNow,
                    DeleteFlag = false

                };
                await unitOfWork.GenericRepository<Domain.Entities.Category>().InsertAsync(entity);
                await unitOfWork.CommitAsync(cancellationToken);
                reponse.Success = true;
                reponse.Message = "Category created successfully";
            }
            catch (Exception ex)
            {
                reponse.Message = ex.Message;
                reponse.Success = false;
                throw new Exception("Category canot create");
            }
            return reponse;


        }

        public async Task<GenericAPIResponse<string>> DeleteCategoryById(Guid id)
        {
            var response = new GenericAPIResponse<string>();
            var cancellationtoken = new CancellationToken();
            var category = await unitOfWork.GenericRepository<Domain.Entities.Category>().GetByIdAsync(id);
            if (category == null)
            {
                response.Success = false;
                response.Message = "Canot delete category";
            }
            else
            {
                await unitOfWork.GenericRepository<Domain.Entities.Category>().DeleteAsync(category);
                await unitOfWork.CommitAsync(cancellationtoken);
                response.Success = true;
                response.Message = "Delete category successfully";
            }
            return response;
        }

        public async Task<List<CategoryDto>> GetAllCategory()
        {

            try
            {

                var query = unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result.
                    Where(x => x.DeleteFlag == false)
                    .Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreatedOn = x.CreatedOn,
                        DeleteFlag = x.DeleteFlag

                    }).ToList();
                return query;

            }
            catch (Exception ex)
            {

            }
            return new List<CategoryDto>();
        }


        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            var category = await unitOfWork.GenericRepository<Domain.Entities.Category>().GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException("Category not found!!");
            }

            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                DeleteFlag = category.DeleteFlag
            };
            return response;
        }


        public async Task<GenericAPIResponse<string>> UpdateCategory(CategoryDto request)
        {
            var cancellationToken = new CancellationToken();
            var response = new GenericAPIResponse<string>();
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("Category Name is invalid");
            }
            try
            {
                var category = await unitOfWork.GenericRepository<Domain.Entities.Category>().GetByIdAsync(request.Id);
                if (category != null)
                {
                    var alreadyexist = unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result
                        .Where(x => x.Name == request.Name);
                    if (alreadyexist.Any())
                    {
                        throw new Exception("Category Name is already exists");
                    }


                }
                category.Name = request.Name;
                category.ModifiedOn = DateTime.UtcNow;
                unitOfWork.GenericRepository<Domain.Entities.Category>().Update(category);
                await unitOfWork.CommitAsync(cancellationToken);
                response.Success = true;
                response.Message = "Update Successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw ex;
            }
            return response;
        }

        public async Task<List<CategoryResponseDto>> GetCategoriesItems()
        {
            try
            {
                var categories = await unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result.ToListAsync();
                var items = await unitOfWork.GenericRepository<Domain.Entities.Item>().GetAllAsync().Result.ToListAsync();
                var categoriesItems = await unitOfWork.GenericRepository<Domain.Entities.CategoryItem>().GetAllAsync().Result.ToListAsync();
                List<CategoryResponseDto> response = new List<CategoryResponseDto>();
                response = (from c in categories
                            join ci in categoriesItems
                            on c.Id equals ci.CategoryId
                            select new CategoryResponseDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                                CreatedOn = c.CreatedOn,
                                items = (from ci2 in categoriesItems
                                         join i in items
                                         on ci2.ItemId equals i.Id
                                         where ci2.CategoryId == c.Id
                                         select new CategoriesItems
                                         {
                                             Id = i.Id,
                                             Name = i.Name,
                                             Description = i.Description,
                                             Price = i.Price,
                                             Quantity = i.Quantity,
                                             ThresholdQuantity = i.ThresholdQuantity,
                                             ImageUrl = i.ImageUrl,
                                             IsAvailable = i.IsAvailable,
                                             DeleteFlag = i.DeleteFlag,
                                             CreatedOn = i.CreatedOn

                                         }


                                         ).ToList()


                            }

                             ).ToList();
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch categories items,{ex.Message}");
            }
        }
    }

}


