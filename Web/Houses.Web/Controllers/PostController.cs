﻿using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Post;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ExceptionMessages;
using static Houses.Common.GlobalConstants.ValidationConstants;

namespace Houses.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public PostController(
            IPostService postService,
            IUserService userService,
            ILogger<PostController> logger)
        {
            _postService = postService;
            _userService = userService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> AllPost(string propertyId, PostServiceViewModel model)
        {
            try
            {
                ViewBag.Title = "All posts";

                var result = await _postService.GetAllByPropertyIdAsync(propertyId);

                model.Posts = result!.Posts;

                if (model == null)
                {
                    throw new NullReferenceException(
                        string.Format(PostsNotFound));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(AllPost));

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string content, string propertyId)
        {
            if (string.IsNullOrEmpty(content))
            {
                return RedirectToAction(nameof(AllPost), new { propertyId });
            }

            string userId = await _userService.GetUserId(User.Id());

            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            await _postService.CreateAsync(content, userId, propertyId);

            return RedirectToAction(nameof(AllPost), new { propertyId });
        }

        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();

            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            IEnumerable<PostServiceViewModel> myPosts = await _postService.GetAllByIdAsync(userId);

            if (myPosts == null)
            {
                throw new NullReferenceException(
                    string.Format(PropertiesNotFound));
            }

            return View(myPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            if (await _postService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(Mine));
            }

            var post = await _postService.GetPostAsync(id);

            if (post == null)
            {
                throw new ArgumentException(
                    string.Format(PostNotFound, id));
            }

            var model = new CreatePostInputViewModel
            {
                Sender = post.Sender!,
                Content = post.Content
            };

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePostInputViewModel postToUpdate, string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            if (postToUpdate == null)
            {
                throw new ArgumentException(
                    string.Format(PostNotFound, id));
            }

            if (!ModelState.IsValid)
            {
                return View(postToUpdate);
            }

            await _postService.EditAsync(id, postToUpdate);

            return RedirectToAction(nameof(Mine));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            if (await _postService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(AllPost));
            }

            var post = await _postService.GetPostAsync(id);

            var model = new PostInputViewModel
            {
                Sender = post.Sender!,
                Content = post.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, PostInputViewModel model)
        {
            if (await _postService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(Mine));
            }

            await _postService.DeletePostAsync(id);

            return RedirectToAction("All", "Property");
        }
    }
}
