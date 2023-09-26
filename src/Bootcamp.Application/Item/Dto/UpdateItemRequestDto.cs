using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Item.Dto
{
    public class UpdateItemRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int ThresholdQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public List<Guid> Categories { get; set; }
    }
}
