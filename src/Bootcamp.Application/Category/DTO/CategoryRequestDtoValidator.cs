using Bootcamp.Application.Item.Dto;
using Bootcamp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Category.DTO
{
    public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
    {
       public CategoryRequestDtoValidator() {
            RuleFor(item => item.Name)
                      .MaximumLength(50)
                      .NotEmpty()
                      .WithMessage("Name must be specified");
        }
       
    }
}
