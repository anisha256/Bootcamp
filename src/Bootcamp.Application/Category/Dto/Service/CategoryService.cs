using Bootcamp.Application.Category.DTO;
using Bootcamp.Application.Category.Interface;
using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Interfaces.Repository;
using Bootcamp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;

namespace Bootcamp.Application.Category.Dto.Service
{
    public class CategoryService : ICategoryservice
    {
        private  readonly IUnitOfWork unitOfWork;
         public CategoryService( IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        
        }
        public async Task<GenericAPIResponse<string>> CreateCategory(CategoryRequestDto request)
        {
            var cancellationToken = new CancellationToken();
            var  reponse=new GenericAPIResponse<string>();
            try
            {
                var entity = new Domain.Entities.Category
                {
                    Name = request.Name,
                    CreatedOn = DateTime.UtcNow,
                     DeleteFlag=false

                };
                await unitOfWork.GenericRepository<Domain.Entities.Category>().InsertAsync(entity);
                await unitOfWork.CommitAsync(cancellationToken);
                reponse.Success = true;
                reponse.Message = "Category created successfully";
            }
             catch (Exception ex)
            {
                reponse.Message = ex.Message;
                reponse.Success=false;
                throw new Exception("Category canot create");
            }
             return reponse;
            
            
        }

        public async Task<GenericAPIResponse<string>> DeleteCategoryById(Guid id)
        {
             var response=new GenericAPIResponse<string>();
             var cancellationtoken= new CancellationToken();
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
                
                var query=    unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result.
                    Where(x=>x.DeleteFlag==false)
                    .Select(x=> new CategoryDto
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
   
            return new CategoryDto
            {Id = category.Id, 
               Name = category.Name,
               CreatedOn = category.CreatedOn,
               DeleteFlag=category.DeleteFlag
            };
            
        }

        public async Task<GenericAPIResponse<string>> UpdateCategory(CategoryDto request)
        {
            var cancellationToken = new CancellationToken();
             var response= new GenericAPIResponse<string>();
            if (string.IsNullOrEmpty(request.Name) )
                {
                throw new Exception("Category Name is invalid");
            }
            try
            {
                var category =  await unitOfWork.GenericRepository<Domain.Entities.Category>().GetByIdAsync(request.Id);
                if (category != null)
                {
                    var alreadyexist = unitOfWork.GenericRepository<Domain.Entities.Category>().GetAllAsync().Result
                        .Where(x => x.Name == request.Name);
                    if (alreadyexist.Any())
                    {
                        throw new Exception("Category Name is already exists");
                    }
       
                        
                }
                category.Name=request.Name;
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
    }
}
