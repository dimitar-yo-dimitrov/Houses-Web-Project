using Houses.Common.GlobalConstants;
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

        public async Task<bool> ExistsById(string id)
        {
            return await _repository
                .All<ApplicationUser>()
                .AnyAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await _repository.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();
        }

        public async Task<EditUserInputViewModel> GetUserForEdit(string id)
        {
            var user = await _repository.GetByIdAsync<ApplicationUser>(id);

            if (user == null)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UserNotFound, id));
            }

            return new EditUserInputViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
            };
        }

        public async Task<IEnumerable<UserServiceViewModel>> GetUserByIdForProfile(string id)
        {
            return await _repository.All<ApplicationUser>()
                .Where(au => au.Id == id)
                .Select(u => new UserServiceViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ProfilePicture = u.ProfilePicture
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(EditUserInputViewModel model)
        {
            bool result;

            var user = await _repository.GetByIdAsync<ApplicationUser>(model.Id);

            if (user == null)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UserNotFound, model.Id));
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfilePicture = model.ProfilePicture;
            user.UserName = model.FirstName;

            await _repository.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _repository.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<ApplicationUser> GetApplicationUserByUserName(string userName)
        {
            return await _repository.All<ApplicationUser>()
                .Where(u => u.UserName == userName)
                .FirstAsync();
        }

        public async Task<string> GetUserId(string id)
        {
            return ((await _repository.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(au => au.Id == id))?.Id ?? null)!;
        }
    }
}
