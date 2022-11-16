﻿using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyViewModel>> GetAllAsync();

        Task<string> CreateAsync(CreatePropertyViewModel property, string userId);

        Task RemovePropertyFromCollectionAsync(string propertyId);

        Task<Property> GetPropertyAsync(string propertyId);

        Task EditAsync(CreatePropertyViewModel propertyToUpdate, string id);

        Task<IEnumerable<PropertyViewModel>> AllPropertiesByUserIdAsync(string userId);
    }
}
