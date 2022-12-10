using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> ExistsById(string id);

        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<EditUserInputViewModel> GetUserForEdit(string id);

        Task<IEnumerable<UserServiceViewModel>> GetUserByIdForProfile(string id);

        Task<bool> UpdateUser(EditUserInputViewModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task<ApplicationUser> GetApplicationUserByUserName(string email);

        Task<string> GetUserId(string id);

        Task DeleteUserAsync(string userId);
    }
}
