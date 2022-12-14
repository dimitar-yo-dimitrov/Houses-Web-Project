using Houses.Core.ViewModels.Post;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPostService
    {
        Task<PostQueryViewModel> GetAllByPropertyIdAsync(string propertyId);

        Task<PostQueryViewModel> GetAllForAdminAsync();

        Task CreateAsync(string content, string userId, string propertyId);

        Task DeletePostAsync(string id);

        Task<IEnumerable<PostServiceViewModel>> GetAllByIdAsync(string id);

        Task EditAsync(string id, CreatePostInputViewModel model);

        Task<Post> GetPostAsync(string postId);

        Task<bool> ExistsAsync(string postId);
    }
}
