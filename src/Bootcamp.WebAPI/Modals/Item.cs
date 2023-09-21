using Bootcamp.WebAPI.Common;

namespace Bootcamp.WebAPI.Modals
{
    public class Item : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ItemDescription { get; set; } 
        public decimal Price { get; set; }
        public decimal? Quantity { get; set; }
        bool Available { get; set; }
        public string? ItemImageUrl { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
