using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;

namespace Houses.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //builder
            //    .HasOne(p => p.Property)
            //    .WithMany(p => p.Posts)
            //    .HasForeignKey(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(p => p.Content)
                .HasMaxLength(MassageMax)
                .IsRequired();
        }
    }
}
