using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyViewModel>> GetAllAsync();

        Task AddPropertyAsync(AddPropertyViewModel model, string userId);

        Task<IEnumerable<MyPropertyViewModel>> UserPropertiesAsync(string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId, string userId);

        Task AddPropertyToCollectionAsync(string propertyId, string userId);

        Task<Property> GetPropertyByIdAsync<T>(string propertyId);

        Task EditAsync(EditPropertyViewModel editProperty, string userId);
    }
}
