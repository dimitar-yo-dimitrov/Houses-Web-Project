using Houses.Core.ViewModels;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<AllPropertyViewModel>> GetAllAsync();

        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();

        Task AddPropertyAsync(AddPropertyViewModel model);

        Task AddPropertyToMyCollectionAsync(string propertyId, string applicationUserId);

        Task<List<AllPropertyViewModel>> GetMyPropertyAsync(string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId, string applicationUserId);
    }
}
