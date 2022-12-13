using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ExceptionMessages;

namespace Houses.Web.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> All(PostServiceViewModel model)
        {
            ViewBag.Title = "All posts";

            var result = await _postService.GetAllForAdminAsync();

            model.Posts = result!.Posts;

            if (model == null)
            {
                throw new NullReferenceException(
                    string.Format(PostsNotFound));
            }

            return View(model);
        }
    }
}
