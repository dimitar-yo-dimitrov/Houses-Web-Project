using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
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

        public async Task<PostInputViewModel> PostsAsync(string id)
        {
            return await _repository.AllReadonly<Post>()
                .Where(p => p.Id == id)
                .Select(p => new PostInputViewModel
                {
                    Title = p.Title,
                    Date = p.Date,
                    Content = p.Content,
                    AuthorId = p.AuthorId
                })
                .FirstAsync();
        }

        public async Task CreateAsync(string content, string authorId, string receiverId)
        {
            var message = new PostInputViewModel
            {
                Content = content,
                AuthorId = authorId,
                ReceiverId = receiverId,
            };

            await _repository.AddAsync(message);
            await _repository.SaveChangesAsync();
        }
    }
}
