using System.Globalization;
using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Core.ViewModels.Property.Enums;
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

        public async Task<PropertyQueryViewModel> GetAllAsync(
            string? propertyType = null,
            string? city = null,
            string? searchTerm = null,
            PropertySorting sorting = PropertySorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)
        {
            var result = new PropertyQueryViewModel();
            var properties = _repository.AllReadonly<Property>(p => p.IsActive);

            if (string.IsNullOrEmpty(propertyType) == false)
            {
                properties = properties
                    .Where(p => p.PropertyType.Title == propertyType);
            }

            if (string.IsNullOrEmpty(city) == false)
            {
                properties = properties
                    .Where(p => p.City.Name == city);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                properties = properties
                    .Where(p => EF.Functions.Like(p.Title.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Address.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Description.ToLower(), searchTerm));
            }

            properties = sorting switch
            {
                PropertySorting.Newest => properties.OrderBy(p => p.CreatedOn),
                PropertySorting.Oldest => properties.OrderByDescending(p => p.CreatedOn),
                PropertySorting.PriceAscending => properties.OrderBy(p => p.Price),
                PropertySorting.PriceDescending => properties.OrderByDescending(p => p.Price),
                _ => throw new ArgumentOutOfRangeException(nameof(sorting), sorting, null)
            };

            result.Properties = await properties
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(p => new PropertyServiceViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters,
                    ImageUrl = p.ImageUrl,
                    Date = p.CreatedOn.ToString(CultureInfo.InvariantCulture)
                })
                .ToListAsync();

            result.TotalPropertyCount = await properties.CountAsync();

            return result;
        }

        public async Task<string> CreateAsync(CreatePropertyInputModel model, string? userId)
        {
            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = new Property
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
                Address = model.Address,
                SquareMeters = model.SquareMeters,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.UtcNow,
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

        public async Task EditAsync(CreatePropertyInputModel model, string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = await _repository
                .AllReadonly<Property>(p => p.IsActive)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, model.Id));
            }

            property.Title = model.Title;
            property.Description = model.Description;
            property.Address = model.Address;
            property.SquareMeters = model.SquareMeters;
            property.ImageUrl = model.ImageUrl;
            property.Price = model.Price;
            property.PropertyTypeId = model.PropertyTypeId;
            property.CityId = model.CityId;

            _repository.Update(property);

            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyServiceViewModel>> AllPropertiesByUserIdAsync(string userId)
        {
            return await _repository
                .AllReadonly<Property>(p => p.IsActive)
                .Where(p => p.OwnerId == userId)
                .Select(p => new PropertyServiceViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters,
                    ImageUrl = p.ImageUrl,
                })
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string propertyId)
        {
            return await _repository.AllReadonly<Property>()
                .AnyAsync(p => p.Id == propertyId && p.IsActive);
        }

        public async Task<DetailsPropertyServiceModel> PropertyDetailsByIdAsync(string propertyId)
        {
            return await _repository
                .AllReadonly<Property>(p => p.IsActive)
                .Where(p => p.Id == propertyId)
                .Select(p => new DetailsPropertyServiceModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters,
                    ImageUrl = p.ImageUrl,
                    PropertyType = p.PropertyType.Title,
                    User = new ApplicationUser()
                    {
                        FirstName = p.Owner.FirstName,
                        LastName = p.Owner.LastName,
                        Email = p.Owner.Email,
                        PhoneNumber = p.Owner.PhoneNumber,
                        ProfilePicture = p.Owner.ProfilePicture
                    }
                })
                .FirstAsync();
        }

        public async Task<Property> GetPropertyAsync(string propertyId)
        {
            var property = await _repository
                .All<Property>(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PropertyNotFound, propertyId));
            }

            return property;
        }

        public async Task RemovePropertyFromCollectionAsync(string propertyId)
        {
            var property = await _repository.GetByIdAsync<Property>(propertyId);

            if (property == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PropertyNotFound, propertyId));
            }

            property.IsActive = false;

            await _repository.SaveChangesAsync();
        }
    }
}
