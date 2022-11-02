using Houses.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Houses.Infrastructure.Constants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.Email)
                .HasMaxLength(EmailMaxLength)
                .IsRequired();

            builder.HasIndex(au => au.Email)
                .IsUnique();
        }
    }
}
