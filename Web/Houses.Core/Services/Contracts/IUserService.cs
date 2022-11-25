using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> ExistsById(string userId);

        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<EditUserInputViewModel> GetUserForEdit(string id);

        Task<IEnumerable<UserServiceViewModel>> GetUserByName(string author);

        Task<bool> UpdateUser(EditUserInputViewModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task<string> GetUserId(string userId);
    }
}
