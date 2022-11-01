using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Houses.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;

        public virtual DbSet<Post> Posts { get; set; } = null!;

        public virtual DbSet<Image> Images { get; set; } = null!;

        public virtual DbSet<Neighborhood> Neighborhoods { get; set; } = null!;

        public virtual DbSet<Property> Properties { get; set; } = null!;

        public virtual DbSet<PropertyType> PropertyTypes { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Seed

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Properties)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithOne(au => au.PostId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}