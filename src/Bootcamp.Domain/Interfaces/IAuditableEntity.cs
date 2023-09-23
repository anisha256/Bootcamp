using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bootcamp.Domain.Interfaces.IAuditableEntity;

namespace Bootcamp.Domain.Interfaces
{
    public interface IAuditableEntity
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
