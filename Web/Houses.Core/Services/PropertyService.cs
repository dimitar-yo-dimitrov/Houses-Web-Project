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
            return await _repository.AllReadonly<Property>(p => p.IsActive)
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
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name,
                    ImageUrl = p.ImageUrl
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
                PropertyTypeId = model.PropertyTypeId,
                CityId = model.City,
                OwnerId = model.OwnerId
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
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name,
                    ImageUrl = p.ImageUrl,
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

            var property = await _repository.All<Property>()
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property != null)
            {
                property.IsActive = false;

                await _repository.SaveChangesAsync();
            }
        }
    }
}
