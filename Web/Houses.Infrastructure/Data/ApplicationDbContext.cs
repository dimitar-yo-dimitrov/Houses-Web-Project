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
            modelBuilder
                .Entity<PropertyType>()
                .HasData(new PropertyType()
                {
                    Id = 1,
                    Name = "Action"
                },
                    new PropertyType()
                    {
                        Id = 2,
                        Name = "Biography"
                    },
                    new PropertyType()
                    {
                        Id = 3,
                        Name = "Children"
                    },
                    new PropertyType()
                    {
                        Id = 4,
                        Name = "Crime"
                    },
                    new PropertyType()
                    {
                        Id = 5,
                        Name = "Fantasy"
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}