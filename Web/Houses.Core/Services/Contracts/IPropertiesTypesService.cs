using Houses.Core.ViewModels.PropertyType;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertiesTypesService
    {
        public Task<IEnumerable<PropertyTypeViewModel>> GetAllTypesAsync();
    }
}
