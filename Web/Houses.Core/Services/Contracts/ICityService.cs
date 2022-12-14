using Houses.Core.ViewModels.City;

namespace Houses.Core.Services.Contracts
{
    public interface ICityService
    {
        public Task<IEnumerable<string>> AllCityNamesAsync();

        public Task<IEnumerable<CityViewModel>> GetAllCitiesAsync();
    }
}
