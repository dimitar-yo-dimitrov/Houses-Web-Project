using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels;
using Houses.Infrastructure.Data;
using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext _context;

        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AllPropertyViewModel>> GetAllAsync()
        {
            return await _context.Properties
                .Select(p => new AllPropertyViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Address = p.Address,
                    Floor = p.Floor,
                    SquareMeters = p.SquareMeters,
                    Elevator = p.Elevator,
                    ImageUrl = p.Images,
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
            => await _context.PropertyTypes.ToListAsync();

        public async Task AddPropertyAsync(AddPropertyViewModel model)
        {
            var entity = new Property
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
                Address = model.Address,
                Floor = model.Floor,
                SquareMeters = model.SquareMeters,
                Elevator = model.Elevator,
                Images = model.ImageUrl,
                PropertyTypeId = model.PropertyTypeId,
                City = model.City,
            };

            await _context.Properties.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddPropertyToCollectionAsync(string propertyId, string applicationUserId)
        {
            var user = await _context.Users
                .Where(u => u.Id == applicationUserId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var property = await _context.Properties
                .FirstOrDefaultAsync(u => u.Id == propertyId);

            if (property == null)
            {
                throw new ArgumentException("Invalid Property ID");
            }

            if (user.ApplicationUserProperties.All(p => p.PropertyId != propertyId))
            {
                user.ApplicationUserProperties.Add(new ApplicationUserProperty
                {
                    PropertyId = property.Id,
                    ApplicationUserId = user.Id,
                    Property = property,
                    ApplicationUser = user
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AllPropertyViewModel>> GetMyPropertyAsync(string userId)
        {
            return await _context.Properties
                .Where(p => p.ApplicationUserProperties.Any(up => up.ApplicationUserId == userId))
                .Select(p => new AllPropertyViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Address = p.Address,
                    Floor = p.Floor,
                    SquareMeters = p.SquareMeters,
                    Elevator = p.Elevator,
                    ImageUrl = p.Images,
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name
                })
                .ToListAsync();
        }

        public async Task RemovePropertyFromCollectionAsync(string propertyId, string applicationUserId)
        {
            var user = await _context.Users
                .Where(u => u.Id == applicationUserId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var property = user.ApplicationUserProperties
                .FirstOrDefault(p => p.PropertyId == propertyId);

            if (property != null)
            {
                user.ApplicationUserProperties.Remove(property);

                await _context.SaveChangesAsync();
            }
        }
    }
}
