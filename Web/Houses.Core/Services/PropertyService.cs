using System.Globalization;
using System.Text;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
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
                    Price = p.Price.ToString(CultureInfo.InvariantCulture),
                    Description = p.Description,
                    Address = p.Address,
                    SquareMeters = p.SquareMeters.ToString(),
                    PropertyType = p.PropertyType.Title,
                    City = p.City.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();
        }

        public async Task AddPropertyAsync(AddPropertyViewModel model)
        {
            var errors = ValidateProperty(model);

            if (errors.Length > 0)
            {
                throw new ArgumentException(errors);
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
                PropertyTypeId = model.PropertyTypeId
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

            var applicationUserProperty = new ApplicationUserProperty
            {
                PropertyId = property.Id,
                ApplicationUserId = user.Id,
                Property = property,
                ApplicationUser = user
            };

            if (user.ApplicationUserProperties.All(p => p.PropertyId != propertyId))
            {
                await _repository.AddAsync(applicationUserProperty);
                await _repository.SaveChangesAsync();
            }
        }

        public Property GetProperty(string propertyId)
        {
            return _repository.All<Property>().FirstOrDefault(x => x.Id == propertyId)!;
        }

        //public void SaveChanges(AddCardInputModel input, string cardId)
        //{
        //    var errors = this.ValidateCard(input);

        //    if (errors.Length > 0)
        //    {
        //        throw new ArgumentException(errors);
        //    }

        //    var targetCard = this.GetCard(cardId);

        //    targetCard.Name = input.Name;
        //    targetCard.ImageUrl = input.ImageUrl;
        //    targetCard.Keyword = input.Keyword;
        //    targetCard.Attack = int.Parse(input.Attack);
        //    targetCard.Health = int.Parse(input.Health);
        //    targetCard.Description = input.Description;

        //    db.SaveChanges();
        //}

        public async Task<IEnumerable<MyPropertyViewModel>> GetMyPropertyAsync(string propertyId)
        {
            return await _repository.All<Property>()
                .Where(p => p.ApplicationUserProperties.Any(aup => aup.ApplicationUserId == propertyId))
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

        private static string ValidateProperty(AddPropertyViewModel input)
        {
            var errorBuilder = new StringBuilder();

            if (!decimal.TryParse(input.Price, out _) || decimal.Parse(input.Price) < 1.00M && decimal.Parse(input.Price) < 1000000000.00M)
            {
                errorBuilder.AppendLine("The field Price must be between 1.00 and 1000000000.00<br>");
            }

            if (!double.TryParse(input.SquareMeters, out _) || double.Parse(input.SquareMeters) < 1.00 && double.Parse(input.SquareMeters) < 100000.00)
            {
                errorBuilder.AppendLine("The field Price must be between 1.00 and 100000.00<br>");
            }

            return errorBuilder.ToString().TrimEnd();
        }
    }
}
