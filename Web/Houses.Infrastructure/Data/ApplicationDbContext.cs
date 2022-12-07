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
            modelBuilder.ApplyConfiguration(new PropertyTypesConfiguration());
            modelBuilder.ApplyConfiguration(new CitiesConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            //modelBuilder.ApplyConfiguration(new PropertyPostConfiguration());

            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ApplicationUserProperty> ApplicationUserProperties { get; set; } = null!;

        // public virtual DbSet<PropertyPost> PropertiesPosts { get; set; } = null!;

        public virtual DbSet<City> Cities { get; init; } = null!;

        public virtual DbSet<Post> Posts { get; init; } = null!;

        public virtual DbSet<Property> Properties { get; init; } = null!;

        public virtual DbSet<PropertyType> PropertyTypes { get; init; } = null!;
    }
}