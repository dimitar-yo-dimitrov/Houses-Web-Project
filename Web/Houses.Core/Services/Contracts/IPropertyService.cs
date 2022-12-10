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

        Task<string> CreateAsync(CreatePropertyInputModel property, string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId);

        Task<bool> ExistsAsync(string propertyId);

        Task<DetailsPropertyServiceModel> PropertyDetailsByIdAsync(string propertyId);

        //DetailsPropertyServiceModel GetPropertyById(string propertyId);

        Task EditAsync(CreatePropertyInputModel model, string id);

        Task<IEnumerable<PropertyServiceViewModel>> AllPropertiesByUserIdAsync(string userId);
    }
}
