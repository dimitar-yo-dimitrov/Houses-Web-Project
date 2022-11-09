using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.PropertyType;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class PropertyTypeService : IPropertiesTypesService
    {
        private readonly IApplicationDbRepository _repository;

        public PropertyTypeService(IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PropertyTypeViewModel>> GetAllTypesAsync()
        {
            var propertiesTypes = await _repository.AllReadonly<PropertyType>()
                .Select(pt => new PropertyTypeViewModel
                {
                    Id = pt.Id,
                    Name = pt.Title
                })
                .ToListAsync();

            return propertiesTypes;
        }
    }
}
