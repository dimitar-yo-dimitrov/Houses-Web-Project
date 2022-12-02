using Houses.Core.ViewModels.Post;
using Houses.Core.ViewModels.Post.Enums;
using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.Services.Contracts
{
    public interface IPostService
    {
        Task<PostQueryViewModel> GetAllAsync(
            string? searchTerm = null,
            PostSorting sorting = PostSorting.Newest,
            int currentPage = 1,
            int postPerPage = 1);

        Task<string> CreatePostAsync(string id, CreatePostInputViewModel model);

        Task DeletePostAsync(string id);

        Task<IEnumerable<PostInputViewModel>> GetAllByIdAsync(string id);

        Task EditAsync(EditPostInputModel model, string id);

        Task<Post> GetPostAsync(string postId);

        Task<bool> ExistsAsync(string postId);
    }
}
