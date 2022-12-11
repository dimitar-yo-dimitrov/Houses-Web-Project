using Ganss.Xss;
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
        private readonly HtmlSanitizer _sanitizer = new();
        private readonly IApplicationDbRepository _repository;
        private readonly IUserService _userService;

        public PostService(
            IApplicationDbRepository repository,
            IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<PostQueryViewModel> GetAllAsync(string propertyId)
        {
            var result = new PostQueryViewModel();
            var posts = _repository.AllReadonly<Post>(p => p.IsActive);

            result.Posts = await posts
                .Select(p => new PostServiceViewModel
                {
                    PropertyId = p.PropertyId,
                    Sender = p.Sender!,
                    Content = p.Content,
                    Date = p.CreatedOn,
                })
                .ToListAsync();

            return result;
        }

        public async Task CreateAsync(
            string content,
            string userId,
            string propertyId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, userId));
            }

            Post post = new Post
            {
                Sender = user.FirstName,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                AuthorId = userId,
                PropertyId = propertyId
            };

            if (post == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PostsNotFound));
            }

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletePostAsync(string id)
        {
            var post = await _repository.GetByIdAsync<Post>(id);

            if (post == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PostNotFound, id));
            }

            post.IsActive = false;

            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostServiceViewModel>> GetAllByIdAsync(string id)
        {
            return await _repository
                .AllReadonly<Post>(p => p.IsActive)
                .Where(p => p.AuthorId == id)
                .Select(p => new PostServiceViewModel
                {
                    Id = p.Id,
                    Sender = p.Sender!,
                    Content = p.Content,
                    Date = p.CreatedOn,
                })
                .ToListAsync();
        }

        public async Task EditAsync(CreatePostInputViewModel model, string id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var post = await _repository
                .AllReadonly<Post>(p => p.IsActive)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PostNotFound, model.Id));
            }

            post.Sender = model.Sender;
            post.Content = model.Content;
            post.ModifiedOn = DateTime.Now;

            _repository.Update(post);

            await _repository.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(string postId)
        {
            var post = await _repository
                .All<Post>(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PostNotFound, postId));
            }

            return post;
        }

        public async Task<IEnumerable<CreatePostInputViewModel>> GetPostByPropertyId(string propertyId)
        {
            var post = await _repository.All<Post>(p => p.IsActive)
                .Where(p => p.Id == propertyId)
                .Select(p => new CreatePostInputViewModel()
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    Content = p.Content,
                    Sender = $"{p.Author.FirstName} {p.Author.LastName}",
                    CreatedOn = p.CreatedOn,
                    AuthorId = p.AuthorId
                })
                .ToListAsync();

            return post;
        }

        public async Task<bool> ExistsAsync(string postId)
        {
            return await _repository.AllReadonly<Post>()
                .AnyAsync(p => p.Id == postId && p.IsActive);
        }
    }
}
