using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Post;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ExceptionMessages;

namespace Houses.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllPostQueryViewModel queryModel)
        {
            ViewBag.Title = "All posts";

            var result = await _postService.GetAllAsync(
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.CurrentPage,
                AllPostQueryViewModel.PostPerPage);

            queryModel.TotalPostCount = result.TotalPostCount;
            queryModel.Posts = result.Posts;

            if (queryModel == null)
            {
                throw new NullReferenceException(
                    string.Format(PostsNotFound));
            }

            return View(queryModel);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();

            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            IEnumerable<PostInputViewModel> myPosts = await _postService.GetAllByIdAsync(userId);

            if (myPosts == null)
            {
                throw new NullReferenceException(
                    string.Format(PropertiesNotFound));
            }

            return View(myPosts);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = await _userService.GetUserId(User.Id());

            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            string id = await _postService.CreatePostAsync(userId, model);

            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            return RedirectToAction(nameof(All), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            var post = await _postService.GetPostAsync(id);

            if (post == null)
            {
                throw new ArgumentException(
                    string.Format(PostNotFound, id));
            }

            var model = new PostInputViewModel
            {
                AuthorName = post.Author.UserName,
                Content = post.Content
            };

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPostInputModel model, string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            if (model == null)
            {
                throw new ArgumentException(
                    string.Format(PostNotFound, id));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _postService.EditAsync(model, id);

            return RedirectToAction(nameof(All), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _postService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var post = await _postService.GetPostAsync(id);

            var model = new PostInputViewModel
            {
                AuthorName = post.Author.UserName,
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
                return RedirectToAction(nameof(All));
            }

            await _postService.DeletePostAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
