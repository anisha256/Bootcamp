using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Category.DTO
{
    public class CategoryDtoValidator:AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator() {
            RuleFor(item => item.Name)
                         .MaximumLength(50)
                         .NotEmpty()
                         .WithMessage("Name must be specified");
        }
    }
}
