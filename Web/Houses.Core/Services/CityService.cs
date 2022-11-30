using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.City;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IApplicationDbRepository _repository;

        public CityService(IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> AllCityNamesAsync()
        {
            return await _repository
                .AllReadonly<City>()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<CityViewModel>> GetAllCitiesAsync()
        {
            var cities = await _repository.AllReadonly<City>()
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return cities;
        }
    }
}
