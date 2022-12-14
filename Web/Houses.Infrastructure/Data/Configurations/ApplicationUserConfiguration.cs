using Houses.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(au => au.Id)
                .HasMaxLength(EmailMaxLength)
                .IsRequired();

            builder
                .HasIndex(au => au.Email)
                .IsUnique();
        }
    }
}
