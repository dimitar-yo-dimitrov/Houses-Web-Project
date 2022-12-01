using Houses.Core.ViewModels.Post;
using Houses.Infrastructure.Data;
using Houses.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Houses.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All posts";
            var posts = await _context.Posts
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .Select(p => new PostInputViewModel()
                {
                    Title = p.Title,
                    Content = p.Content,
                    Date = p.CreatedOn,
                    AuthorId = p.AuthorId,
                })
                .ToListAsync();

            return View(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreatePost() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePost(PostInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = new()
            {
                Title = model.Title,
                Content = model.Content
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(string id)
        {
            Post post = await _context
                .Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted) ?? new Post();

            if (post == null)
            {
                return BadRequest();
            }

            return View(new PostInputViewModel()
            {
                Title = post.Title,
                Content = post.Content
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(PostInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = await _context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted) ?? new Post();

            if (post == null)
            {
                return BadRequest();
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            Post post = (await _context
                .Posts
                .FirstOrDefaultAsync(p => p.Id == id))!;

            if (post == null)
            {
                return BadRequest();
            }

            post.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
