﻿using Houses.Core.ViewModels.User;

namespace Houses.Core.Services.Contracts
{
    public interface IPostService
    {
        Task<PostInputViewModel> PostsAsync(string id);

        Task CreateAsync(string content, string authorId, string receiverId);
    }
}