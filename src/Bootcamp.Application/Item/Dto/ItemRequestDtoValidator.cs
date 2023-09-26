using FluentValidation;


namespace Bootcamp.Application.Item.Dto
{
    public class ItemRequestDtoValidator : AbstractValidator<ItemRequestDto>
    {
        public ItemRequestDtoValidator()
        {
            RuleFor(item => item.Name)
                    .MaximumLength(20)
                    .NotEmpty()
                    .WithMessage("Name must be specified");
            RuleFor(item => item.Description)
                    .MaximumLength(200);

            RuleFor(item => item.Price)
                    .NotEmpty()
                    .WithMessage("Price is required.")
                    .GreaterThan(0)
                    .WithMessage("Price must be a greater than 0.");
            RuleFor(item => item.Quantity)
                    .GreaterThanOrEqualTo(0)
                 .WithMessage("Quantity must be a non-negative integer.");

            RuleFor(item => item.ImageUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid URL or null.");

            RuleFor(item => item.ThresholdQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("ThresholdQuantity must be a non-negative integer.");

            RuleFor(item => item.IsAvailable)
                .NotNull().WithMessage("IsAvailable must not be null.");

            RuleFor(item => item.Categories)
                .NotNull().WithMessage("Categories must not be null.")
                .NotEmpty().WithMessage("Categories must contain at least one item.");

        }
    }
}
