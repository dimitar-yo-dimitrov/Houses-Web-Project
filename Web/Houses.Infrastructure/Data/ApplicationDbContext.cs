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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());

            modelBuilder.Entity<ApplicationUserProperty>()
                .HasOne(aup => aup.ApplicationUser)
                .WithMany(p => p.ApplicationUserProperties)
                .HasForeignKey(aup => aup.PropertyId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(au => au.Posts)
                .OnDelete(DeleteBehavior.ClientNoAction);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ApplicationUserProperty> ApplicationUserProperties { get; set; } = null!;

        public virtual DbSet<City> Cities { get; init; } = null!;

        public virtual DbSet<Post> Posts { get; init; } = null!;

        public virtual DbSet<Image> Images { get; init; } = null!;

        public virtual DbSet<Neighborhood> Neighborhoods { get; init; } = null!;

        public virtual DbSet<Property> Properties { get; init; } = null!;

        public virtual DbSet<PropertyType> PropertyTypes { get; init; } = null!;
    }
}