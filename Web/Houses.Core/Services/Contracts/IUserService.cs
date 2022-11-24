using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> ExistsById(string userId);

        Task<string> GetUserId(string userId);

        Task<ApplicationUser> GetAppUserByName(string username);

        Task<IEnumerable<UserServiceViewModel>> GetUserByName(string author);

        Task<bool> UpdateUser(EditUserProfileInputModel model);
    }
}
