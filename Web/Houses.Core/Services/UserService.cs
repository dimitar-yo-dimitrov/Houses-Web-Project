using Houses.Core.Services.Contracts;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository _repository;

        public UserService(IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await _repository
                .All<ApplicationUser>()
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<string> GetUserId(string userId)
        {
            return ((await _repository.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(au => au.Id == userId))?.Id ?? null)!;
        }
    }
}
