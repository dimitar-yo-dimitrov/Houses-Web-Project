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

        Task<string> CreateAsync(CreatePostInputViewModel model, string userId);

        Task DeletePostAsync(string id);

        Task<IEnumerable<PostServiceViewModel>> GetAllByIdAsync(string id);

        Task EditAsync(CreatePostInputViewModel model, string id);

        Task<Post> GetPostAsync(string postId);

        Task<IEnumerable<CreatePostInputViewModel>> GetPostByPropertyId(string propertyId);

        Task<bool> ExistsAsync(string postId);
    }
}
