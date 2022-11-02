using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    public class ApplicationUserPropertyConfiguration : IEntityTypeConfiguration<ApplicationUserProperty>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserProperty> builder)
        {
            builder.HasKey(aup => new { aup.ApplicationUserId, aup.PropertyId });
        }
    }
}
