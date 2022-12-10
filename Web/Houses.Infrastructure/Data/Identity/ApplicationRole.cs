using Houses.Infrastructure.Data.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Houses.Infrastructure.Data.Identity
{
    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
            : this(null!)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();
        }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedOn { get; set; }
    }
}
