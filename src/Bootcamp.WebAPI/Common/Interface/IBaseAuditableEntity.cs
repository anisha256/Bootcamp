namespace Bootcamp.WebAPI.Common.Interface
{
    public interface IBaseAuditableEntity
    {
        Guid? CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        Guid? ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        bool DeleteFlag { get; set; }
        Guid? DeletedBy { get; set; }
        DateTime? DeletedOn { get; set; }

    }
}
