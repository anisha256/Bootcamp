using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Dto
{
    public class ItemResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ThresholdQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public List<ItemCategories>? ItemCategories { get; set; }
    }

    public class ItemCategories
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
