using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IApplicationDbRepository _repository;

        public PropertyService(IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PropertyViewModel>> GetAllAsync()
        {
            return await _repository.AllReadonly<Property>()
                .Select(p => new PropertyViewModel
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
            => await _repository.AllReadonly<PropertyType>().ToListAsync();

        public async Task AddPropertyAsync(AddPropertyViewModel model)
        {
            var property = new Property
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

            await _repository.AddAsync(property);
            await _repository.SaveChangesAsync();
        }

        public async Task AddPropertyToMyCollectionAsync(string propertyId, string applicationUserId)
        {
            var user = await _repository.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == applicationUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var property = await _repository.All<Property>()
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

                await _repository.SaveChangesAsync();
            }
        }

        public async Task<List<PropertyViewModel>> GetMyPropertyAsync(string userId)
        {
            return await _repository.All<Property>()
                .Where(p => p.ApplicationUserProperties.Any(up => up.ApplicationUserId == userId))
                .Select(p => new PropertyViewModel()
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
            var user = await _repository.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == applicationUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var property = user.ApplicationUserProperties
                .FirstOrDefault(p => p.PropertyId == propertyId);

            if (property != null)
            {
                user.ApplicationUserProperties.Remove(property);

                await _repository.SaveChangesAsync();
            }
        }
    }
}
