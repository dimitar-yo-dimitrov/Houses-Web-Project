using Houses.Core.ViewModels.Property;
using Houses.Core.ViewModels.Property.Enums;
using Houses.Infrastructure.Data.Entities;

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

        Task<string> CreateAsync(CreatePropertyViewModel property, string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId);

        Task<bool> ExistsAsync(string propertyId);

        Task<DetailsPropertyServiceModel> PropertyDetailsByIdAsync(string propertyId);

        Task<Property> GetPropertyAsync(string propertyId);

        Task EditAsync(CreatePropertyViewModel model, string id);

        Task<IEnumerable<PropertyServiceViewModel>> AllPropertiesByUserIdAsync(string userId);
    }
}
