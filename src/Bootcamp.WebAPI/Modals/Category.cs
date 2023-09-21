using Bootcamp.WebAPI.Common;

namespace Bootcamp.WebAPI.Modals
{
    public class Category : BaseAuditableEntity
    {
  
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<Item>? Items { get; set; }

    }
}
