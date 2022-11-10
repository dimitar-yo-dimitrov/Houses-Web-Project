using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyViewModel>> GetAllAsync();

        Task AddAsync(AddPropertyViewModel property, string userId);

        Task<IEnumerable<MyPropertyViewModel>> GetUserPropertiesAsync(string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId, string userId);

        Task AddPropertyToCollectionAsync(string propertyId, string userId);

        Task<Property> GetPropertyAsync(string propertyId);

        Task EditAsync(EditPropertyViewModel propertyToUpdate, string id);
    }
}
