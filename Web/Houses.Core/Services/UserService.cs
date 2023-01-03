using Ganss.Xss;
using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class UserService : IUserService
    {
        private readonly HtmlSanitizer _sanitizer = new();
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
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _repository.GetByIdAsync<ApplicationUser>(id);

            if (user == null)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UserNotFound, id));
            }

            return new EditUserInputViewModel
            {
                Id = user.Id,
                FirstName = _sanitizer.Sanitize(user.FirstName!),
                LastName = _sanitizer.Sanitize(user.LastName!),
                Email = _sanitizer.Sanitize(user.Email),
                PhoneNumber = _sanitizer.Sanitize(user.PhoneNumber),
                ProfilePicture = _sanitizer.Sanitize(user.ProfilePicture!),
            };
        }

        public async Task<IEnumerable<UserServiceViewModel>> GetUserByIdForProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

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

            user.FirstName = _sanitizer.Sanitize(model.FirstName);
            user.LastName = _sanitizer.Sanitize(model.LastName);
            user.Email = _sanitizer.Sanitize(model.Email);
            user.PhoneNumber = _sanitizer.Sanitize(model.PhoneNumber);
            user.ProfilePicture = _sanitizer.Sanitize(model.ProfilePicture!);
            user.UserName = _sanitizer.Sanitize(model.FirstName);

            await _repository.SaveChangesAsync();

            result = true;

            return result;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return await _repository
                .GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<string> GetUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return ((await _repository.AllReadonly<ApplicationUser>(u => u.IsActive)
                .FirstOrDefaultAsync(au => au.Id == id))?.Id ?? null)!;
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
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
