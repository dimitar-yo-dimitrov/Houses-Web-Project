using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
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

        public async Task<ApplicationUser> GetAppUserByName(string name)
        {
            return await _repository.AllReadonly<ApplicationUser>()
                .Where(u => u.FirstName == name)
                .FirstAsync();
        }

        public async Task<IEnumerable<UserServiceViewModel>> GetUserByName(string author)
        {
            return await _repository.All<ApplicationUser>()
                .Where(au => au.FirstName == author)
                .Select(u => new UserServiceViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(EditUserProfileInputModel model)
        {
            bool result = false;

            var user = await _repository.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;

                user.LastName = model.LastName;

                user.Email = model.Email;

                user.ProfilePicture = model.ProfilePicture;

                await _repository.SaveChangesAsync();

                result = true;
            }

            return result;
        }
    }
}
