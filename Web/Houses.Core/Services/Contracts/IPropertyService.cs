using Houses.Core.ViewModels.Property;
using Houses.Core.ViewModels.Property.Enums;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<PropertyQueryViewModel> GetAllAsync(
            string? propertyType = null,
            string? city = null,
            string? searchTerm = null,
            PropertySorting sorting = PropertySorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<string> CreateAsync(string userId, CreatePropertyInputModel property);

        Task RemovePropertyFromCollectionAsync(string propertyId);

        Task<bool> ExistAsync(string propertyId);

        Task<DetailsPropertyServiceModel> PropertyDetailsByIdAsync(string propertyId);

        Task EditAsync(string? id, CreatePropertyInputModel model);

        Task<IEnumerable<PropertyServiceViewModel>> AllPropertiesByUserIdAsync(string userId);
    }
}
