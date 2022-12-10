using Ganss.Xss;
using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Post;
using Houses.Core.ViewModels.Post.Enums;
using Houses.Infrastructure.Data.Entities;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Houses.Core.Services
{
    public class PostService : IPostService
    {
        private readonly HtmlSanitizer _sanitizer = new();
        private readonly IApplicationDbRepository _repository;

        public PostService(
            IApplicationDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<PostQueryViewModel> GetAllAsync(
            string? searchTerm = null,
            PostSorting sorting = PostSorting.Newest,
            int currentPage = 1,
            int postPerPage = 1)
        {
            var result = new PostQueryViewModel();
            var posts = _repository.AllReadonly<Post>(p => p.IsActive);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                posts = posts
                    .Where(p => EF.Functions.Like(p.Author.UserName.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Content.ToLower(), searchTerm));
            }

            posts = sorting switch
            {
                PostSorting.Newest => posts.OrderBy(p => p.CreatedOn),
                PostSorting.Oldest => posts.OrderByDescending(p => p.CreatedOn),
                _ => throw new ArgumentOutOfRangeException(nameof(sorting), sorting, null)
            };

            result.Posts = await posts
                .Skip((currentPage - 1) * postPerPage)
                .Take(postPerPage)
                .Select(p => new PostServiceViewModel
                {
                    Id = p.Id,
                    Sender = p.Sender,
                    Content = p.Content,
                    Date = p.CreatedOn
                })
                .ToListAsync();

            result.TotalPostCount = await posts.CountAsync();

            return result;
        }

        public async Task<string> CreateAsync(CreatePostInputViewModel model, string userId)
        {
            var property = await _repository
                .AllReadonly<Property>(p => p.IsActive)
                .FirstOrDefaultAsync();

            var post = new Post
            {
                Id = model.Id,
                Sender = model.Sender,
                Content = model.Content,
                AuthorId = userId,
                CreatedOn = DateTime.Now,
                PropertyId = property!.Id
            };

            if (post == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PostsNotFound));
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
                    Sender = p.Sender,
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
                .Select(p => new CreatePostInputViewModel
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
