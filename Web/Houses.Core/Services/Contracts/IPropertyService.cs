using Houses.Core.ViewModels.Property;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyViewModel>> GetAllAsync();

        Task AddPropertyAsync(AddPropertyViewModel model);

        Task AddPropertyToMyCollectionAsync(string propertyId, string applicationUserId);

        Task<IEnumerable<MyPropertyViewModel>> GetMyPropertyAsync(string propertyId);

        Task RemovePropertyFromCollectionAsync(string propertyId, string applicationUserId);
    }
}
