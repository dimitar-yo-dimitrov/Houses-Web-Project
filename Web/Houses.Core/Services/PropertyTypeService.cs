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

        public async Task<IEnumerable<string>> AllPropertyTypeNamesAsync()
        {
            return await _repository.AllReadonly<PropertyType>()
                .Distinct()
                .OrderBy(pt => pt.Title)
                .Select(pt => pt.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<PropertyTypeViewModel>> AllPropertyTypesAsync()
        {
            return await _repository.AllReadonly<PropertyType>()
                .Select(pt => new PropertyTypeViewModel
                {
                    Id = pt.Id,
                    Name = pt.Title
                })
                .ToListAsync();
        }
    }
}
