using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            IApplicationDbRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<bool> ExistsById(string id)
        {
            return await _repository
                .All<ApplicationUser>()
                .AnyAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await _repository.All<ApplicationUser>(u => u.IsActive)
                .Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = $"{u.FirstName} {u.LastName}"
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
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
            };
        }

        public async Task<IEnumerable<UserServiceViewModel>> GetUserByIdForProfile(string id)
        {
            return await _repository.All<ApplicationUser>(u => u.IsActive)
                .Where(au => au.Id == id)
                .Select(u => new UserServiceViewModel
                {
                    FirstName = u.FirstName!,
                    LastName = u.LastName!,
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
            return await _repository
                .GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<ApplicationUser> GetApplicationUserByUserName(string userName)
        {
            return await _repository.All<ApplicationUser>(u => u.IsActive)
                .Where(u => u.UserName == userName)
                .FirstAsync();
        }

        public async Task<string> GetUserId(string id)
        {
            return ((await _repository.AllReadonly<ApplicationUser>(u => u.IsActive)
                .FirstOrDefaultAsync(au => au.Id == id))?.Id ?? null)!;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null/* || user.IsActive == false*/)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.UserNotFound, userId));
            }

            user.FirstName = null!;
            user.LastName = null!;
            user.UserName = null;
            user.NormalizedUserName = null;
            user.Email = null;
            user.NormalizedEmail = null;
            user.EmailConfirmed = false;
            user.PasswordHash = null;
            user.SecurityStamp = null;
            user.ConcurrencyStamp = null;
            user.PhoneNumber = null;
            user.PhoneNumberConfirmed = false;
            user.TwoFactorEnabled = false;
            user.LockoutEnd = null;
            user.LockoutEnabled = false;
            user.AccessFailedCount = 0;
            user.ProfilePicture = null;
            user.IsActive = false;

            var userProperties = await _repository
                .All<Property>(p => p.IsActive && p.OwnerId == userId)
                .ToListAsync();

            var userPosts = await _repository
                .All<Post>(p => p.IsActive && p.AuthorId == userId)
                .ToListAsync();

            foreach (var property in userProperties)
            {
                property.IsActive = false;
            }

            foreach (var post in userPosts)
            {
                post.IsActive = false;
            }

            await _repository.SaveChangesAsync();
        }
    }
}
