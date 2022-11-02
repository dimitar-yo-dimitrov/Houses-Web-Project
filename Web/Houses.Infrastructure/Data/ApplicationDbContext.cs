using Houses.Infrastructure.Data.Configuration;
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


        public virtual DbSet<Property> Properties { get; set; } = null!;

        public virtual DbSet<PropertyType> PropertyTypes { get; set; } = null!;

        public virtual DbSet<ApplicationUserProperty> ApplicationUserProperties { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Seed

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserPropertyConfiguration());

            //modelBuilder.Entity<Neighborhood>()
            //    .HasOne(n => n.City)
            //    .WithMany()
            //    .HasForeignKey(n => n.CityId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithOne(au => au.PostId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}