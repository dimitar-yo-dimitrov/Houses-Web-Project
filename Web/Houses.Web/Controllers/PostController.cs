using Houses.Core.Services.Contracts;
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

                model.Posts = result.Posts;

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
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, "Content return false in {0}", DateTime.Now);

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
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Create));

                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> Mine()
        {
            try
            {
                var userId = User.Id();

                _logger.LogInformation(MyLogEvents.GetId, "Getting id {0} at {1}", userId, DateTime.Now);

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
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Mine));

                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new NullReferenceException(
                        string.Format(IdIsNull));
                }

                if (await _postService.ExistsAsync(id) == false)
                {
                    _logger.LogWarning(MyLogEvents.GetId, "ExistAsync() return false in {0}", DateTime.Now);

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
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Edit));

                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePostInputViewModel postToUpdate, string? id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Edit));

                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new NullReferenceException(
                        string.Format(IdIsNull));
                }

                if (await _postService.ExistsAsync(id) == false)
                {
                    _logger.LogWarning(MyLogEvents.DeleteItem, "ExistAsync() return false in {0}", DateTime.Now);

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
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Delete));

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, PostInputViewModel model)
        {
            try
            {
                if (await _postService.ExistsAsync(id) == false)
                {
                    _logger.LogWarning(MyLogEvents.DeleteItem, "ExistAsync() return false in {0}", DateTime.Now);

                    return RedirectToAction(nameof(Mine));
                }

                await _postService.DeletePostAsync(id);

                return RedirectToAction("All", "Property");
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, "Something went wrong: {ex}", nameof(Delete));

                return NotFound(ex.Message);
            }
        }
    }
}
