
namespace Bootcamp.Domain.Entities
{
    public class Item : AuditableEnttity
    {
        public Item()
        {
            this.CategoryItems = new HashSet<CategoryItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public string? ImageUrl { get; set; }
        public int ThresholdQuantity { get; set; } = 0;
        public bool IsAvailable { get; set; } = false;  
        public ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
