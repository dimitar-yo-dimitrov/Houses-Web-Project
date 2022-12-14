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

        public async Task<PostQueryViewModel> GetAllByPropertyIdAsync(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var result = new PostQueryViewModel();
            var posts = _repository
                .AllReadonly<Post>(p => p.IsActive && p.PropertyId == propertyId);

            result.Posts = await posts
                .Select(p => new PostServiceViewModel
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    Sender = p.Sender!,
                    Content = p.Content,
                    Date = p.CreatedOn,
                    AuthorId = p.Author.Id
                })
                .ToListAsync();

            return result;
        }

        public async Task<PostQueryViewModel> GetAllForAdminAsync()
        {
            var result = new PostQueryViewModel();
            var posts = _repository
                .AllReadonly<Post>(p => p.IsActive);

            result.Posts = await posts
                .Select(p => new PostServiceViewModel
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    Sender = p.Sender!,
                    Content = p.Content,
                    Date = p.CreatedOn,
                    AuthorId = p.Author.Id
                })
                .ToListAsync();

            return result;
        }

        public async Task CreateAsync(
            string content,
            string userId,
            string propertyId)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            if (string.IsNullOrEmpty(propertyId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, userId));
            }

            var post = new Post
            {
                Sender = user.FirstName,
                Content = _sanitizer.Sanitize(content),
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
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

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
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

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

        public async Task EditAsync(string id, CreatePostInputViewModel model)
        {
            if (string.IsNullOrEmpty(id))
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

            post.Sender = _sanitizer.Sanitize(model.Sender);
            post.Content = _sanitizer.Sanitize(model.Content);
            post.ModifiedOn = DateTime.Now;

            _repository.Update(post);

            await _repository.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var post = await _repository
                .All<Post>(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PostNotFound, postId));
            }

            return post;
        }

        public async Task<bool> ExistsAsync(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return await _repository.AllReadonly<Post>()
                .AnyAsync(p => p.Id == postId && p.IsActive);
        }
    }
}
