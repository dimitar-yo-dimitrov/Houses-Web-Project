using Houses.Core.ViewModels.Post;

namespace Houses.Core.Services.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostInputViewModel>> GetAllAsync();

        Task<string> CreatePostAsync(string id, PostInputViewModel model);

        Task DeletePostAsync(string id);

        Task<PostInputViewModel> GetByIdAsync(string id);

        Task EditAsync(EditPostInputModel model, string id);
    }
}
