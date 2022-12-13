using Ganss.Xss;
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
        private readonly HtmlSanitizer _sanitizer = new();
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
                PropertySorting.Newest => properties.OrderByDescending(p => p.CreatedOn),
                PropertySorting.Oldest => properties.OrderBy(p => p.CreatedOn),
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
                    Date = p.CreatedOn,
                    OwnerId = p.OwnerId
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
                Title = _sanitizer.Sanitize(model.Title),
                Price = model.Price,
                Description = _sanitizer.Sanitize(model.Description),
                Address = _sanitizer.Sanitize(model.Address),
                SquareMeters = model.SquareMeters,
                ImageUrl = _sanitizer.Sanitize(model.ImageUrl),
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

            property.Title = _sanitizer.Sanitize(model.Title);
            property.Description = _sanitizer.Sanitize(model.Description);
            property.Address = _sanitizer.Sanitize(model.Address);
            property.SquareMeters = model.SquareMeters;
            property.ImageUrl = _sanitizer.Sanitize(model.ImageUrl);
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
                    OwnerId = p.OwnerId
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
            try
            {
                var property = await _repository
                    .All<Property>(p => p.IsActive)
                    .FirstOrDefaultAsync(p => p.Id == propertyId);

                var propertyToReturn = new PropertyServiceViewModel
                {
                    Id = property!.Id,
                    Description = property.Description,
                    Address = property.Address,
                    Title = property.Title,
                    ImageUrl = property.ImageUrl,
                    Price = property.Price,
                    SquareMeters = property.SquareMeters,
                    PropertyTypeId = property.PropertyTypeId,
                    PropertyType = property.PropertyType.Title,
                    CityId = property.CityId,
                    User = new ApplicationUser
                    {
                        FirstName = property.Owner.FirstName,
                        LastName = property.Owner.LastName,
                        Email = property.Owner.Email,
                        PhoneNumber = property.Owner.PhoneNumber,
                        ProfilePicture = property.Owner.ProfilePicture
                    }
                };

                var multiModel = new DetailsPropertyServiceModel
                {
                    PropertyDto = propertyToReturn,
                };

                return multiModel;

            }
            catch (ArgumentNullException)
            {
                return null!;
            }
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
