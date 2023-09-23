
using Bootcamp.Domain.Entities;
using Bootcamp.WebAPI.Modals;

namespace Bootcamp.Domain.Entities
{
    public class CategoryItem : AuditableEnttity
    {
        public CategoryItem()
        {

        }
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
