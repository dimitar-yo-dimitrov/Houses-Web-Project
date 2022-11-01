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
                Neighborhood = model.Neighborhood,
            };

            await _context.Properties.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddPropertyToCollectionAsync(int propertyId, string applicationUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AllPropertyViewModel>> GetMyPropertyAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePropertyFromCollectionAsync(int propertyId, string applicationUserId)
        {
            throw new NotImplementedException();
        }
    }
}
