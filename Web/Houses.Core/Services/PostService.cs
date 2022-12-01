using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Post;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IApplicationDbRepository _repository;

        public PostService(IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PostInputViewModel>> GetAllAsync()
        {
            var post = await _repository.AllReadonly<Post>()
                .Select(p => new PostInputViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Date = p.CreatedOn,
                    AuthorId = p.Author.UserName
                })
                .ToListAsync();

            return post;
        }

        public async Task<string> CreatePostAsync(string id, PostInputViewModel model)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var post = new Post
            {
                Title = model.Title,
                CreatedOn = DateTime.UtcNow,
                Content = model.Content,
                AuthorId = id
            };

            if (post == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, string.Empty));
            }

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();

            return model.Id;
        }

        public async Task DeletePostAsync(string id)
        {
            var post = await _repository.GetByIdAsync<Post>(id);

            if (post == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PostNotFound, id));
            }

            post.IsDeleted = false;

            await _repository.SaveChangesAsync();
        }

        public Task<PostInputViewModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task EditAsync(EditPostInputModel model, string id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var post = await _repository
                .AllReadonly<Post>(p => p.IsDeleted)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PostNotFound, model.Id));
            }

            post.Title = model.Title;
            post.Content = model.Content;

            _repository.Update(post);

            await _repository.SaveChangesAsync();
        }
    }
}
