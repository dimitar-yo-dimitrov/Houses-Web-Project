//using Houses.Infrastructure.Data.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Houses.Infrastructure.Data.Configuration
//{
//    public class PropertyPostConfiguration : IEntityTypeConfiguration<PropertyPost>
//    {
//        public void Configure(EntityTypeBuilder<PropertyPost> builder)
//        {
//            builder
//                .HasKey(aup => new { aup.PropertyId, aup.PostId });

//            builder
//                .HasOne(aup => aup.Property)
//                .WithMany(p => p.Posts)
//                .HasForeignKey(aup => aup.PostId)
//                .OnDelete(DeleteBehavior.NoAction);
//        }
//    }
//}