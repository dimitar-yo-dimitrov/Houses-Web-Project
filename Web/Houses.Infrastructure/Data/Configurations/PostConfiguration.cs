using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasOne(p => p.Author)
                .WithMany(au => au.Posts)
                .HasForeignKey(p => p.AuthorId);
            //.OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
