using System.Globalization;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Core.ViewModels.Property.Enums;
using Houses.Infrastructure.Data.Entities;
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

        public async Task<PropertyQueryViewModel> GetAllAsync(string? propertyType = null,
            string? searchTerm = null,
            PropertySorting sorting = PropertySorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)
        {
            var result = new PropertyQueryViewModel();
            var properties = _repository.AllReadonly<Property>();

            if (string.IsNullOrEmpty(propertyType) == false)
            {
                properties = properties
                    .Where(p => p.Title == propertyType);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm!.ToLower()}%";

                properties = properties
                    .Where(p => EF.Functions.Like(p.Title.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Address.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Description.ToLower(), searchTerm));
            }

            properties = sorting switch
            {
                PropertySorting.Newest => properties.OrderBy(p => p.Id),
                PropertySorting.Price => properties.OrderBy(p => p.Price),
                _ => properties.OrderByDescending(p => p.Id)
            };

            result.Properties = await properties
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(p => new PropertyServiceViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price.ToString(CultureInfo.InvariantCulture),
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters.ToString(),
                    ImageUrl = p.ImageUrl,
                })
                .ToListAsync();

            result.TotalPropertyCount = await properties.CountAsync();

            return result;
        }

        public async Task<string> CreateAsync(CreatePropertyViewModel model, string? userId)
        {
            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = new Property
            {
                Title = model.Title,
                Price = Convert.ToDecimal(model.Price),
                Description = model.Description,
                Address = model.Address,
                SquareMeters = double.Parse(model.SquareMeters!),
                ImageUrl = model.ImageUrl,
                CityId = model.CityId,
                PropertyTypeId = model.PropertyTypeId,
                OwnerId = userId
            };

            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, string.Empty));
            }

            await _repository.AddAsync(property);
            await _repository.SaveChangesAsync();

            return model.Id;
        }

        public async Task EditAsync(CreatePropertyViewModel propertyToUpdate, string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = await _repository
                .AllReadonly<Property>()
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

        public async Task<IEnumerable<PropertyViewModel>> AllPropertiesByUserIdAsync(string userId)
        {
            return await _repository
                .All<Property>()
                .Where(p => p.ApplicationUserProperties.Any(aup => aup.ApplicationUserId == userId))
                .OrderBy(p => p.Title)
                .Select(p => new PropertyViewModel
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
                    Owner = p.OwnerId

                })
                .ToListAsync();
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

        public async Task RemovePropertyFromCollectionAsync(string propertyId)
        {
            var property = await _repository.All<Property>()
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PropertyNotFound, propertyId));
            }


            property.IsActive = false;

            await _repository.SaveChangesAsync();
        }
    }
}
