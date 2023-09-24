
using Bootcamp.Domain.Entities;
using Bootcamp.WebAPI.Modals;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bootcamp.Domain.Entities
{
    public class CategoryItem : AuditableEnttity
    {
        public CategoryItem()
        {

        }
        public Guid Id { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
