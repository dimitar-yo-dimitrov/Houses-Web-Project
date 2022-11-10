using System.Globalization;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.Data.Repositories;
using Houses.Infrastructure.GlobalConstants;
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
            return await _repository
                .AllReadonly<Property>(p => p.IsActive)
                .Select(p => new PropertyViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price.ToString(CultureInfo.InvariantCulture),
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters.ToString(),
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name,
                    ImageUrl = p.ImageUrl
                })
                .OrderBy(p => p.Title)
                .ToListAsync();
        }

        public async Task AddAsync(AddPropertyViewModel model, string userId)
        {
            var property = new Property
            {
                Title = model.Title,
                Price = Convert.ToDecimal(model.Price),
                Description = model.Description,
                Address = model.Address,
                SquareMeters = double.Parse(model.SquareMeters!),
                ImageUrl = model.ImageUrl,
                CityId = model.CityId,
                PropertyTypeId = model.PropertyTypeId
            };


            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, string.Empty));
            }

            await _repository.AddAsync(property);
            //await _repository.AddAsync(new ApplicationUserProperty { ApplicationUserId = userId, PropertyId = property.Id });

            await _repository.SaveChangesAsync();
        }

        public async Task EditAsync(EditPropertyViewModel propertyToUpdate, string id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = await _repository
                .All<Property>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, propertyToUpdate.Id));
            }

            property.Title = propertyToUpdate.Title;
            property.Description = propertyToUpdate.Description;
            property.Address = propertyToUpdate.Address;
            property.SquareMeters = double.Parse(propertyToUpdate.SquareMeters!);
            property.ImageUrl = propertyToUpdate.ImageUrl;
            property.Price = Convert.ToDecimal(propertyToUpdate.Price);
            property.PropertyTypeId = propertyToUpdate.PropertyTypeId;
            property.CityId = propertyToUpdate.CityId;

            _repository.Update(property);

            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyPropertyViewModel>> GetUserPropertiesAsync(string userId)
        {
            return await _repository
                .All<Property>()
                .Where(p => p.ApplicationUserProperties.Any(aup => aup.ApplicationUserId == userId))
                .Select(p => new MyPropertyViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price.ToString(CultureInfo.CurrentCulture),
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters.ToString(),
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name,
                    ImageUrl = p.ImageUrl,
                })
                .OrderBy(p => p.Title)
                .ToListAsync();
        }

        public async Task AddPropertyToCollectionAsync(string propertyId, string userId)
        {
            var property = _repository
                .All<Property>()
                .FirstOrDefaultAsync(p => p.ApplicationUserProperties
                    .Any(aup => aup.PropertyId == propertyId && aup.ApplicationUserId == userId));

            if (property == null)
            {
                throw new ArgumentException(ExceptionMessages.AddPropertyToCollectionNotFound);
            }

            await _repository.AddAsync(new ApplicationUserProperty { ApplicationUserId = userId, PropertyId = propertyId });

            await _repository.SaveChangesAsync();
        }

        public async Task<Property> GetPropertyAsync(string propertyId)
        {
            var property = await _repository
                .All<Property>()
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PropertyNotFound, propertyId));
            }

            return property;
        }

        public async Task RemovePropertyFromCollectionAsync(string propertyId, string applicationUserId)
        {
            var user = await _repository
                .All<ApplicationUser>()
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
