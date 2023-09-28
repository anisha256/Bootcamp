using Bootcamp.Domain.Entities;

namespace Bootcamp.Application.Category.Dto
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<CategoriesItems>? items { get; set; }
    }

    public class CategoriesItems 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ThresholdQuantity { get; set; } 
        public bool IsAvailable { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
