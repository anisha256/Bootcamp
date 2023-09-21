using Bootcamp.WebAPI.Common.Interface;

namespace Bootcamp.WebAPI.Common
{
    public abstract class BaseAuditableEntity : IBaseAuditableEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool DeleteFlag { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
