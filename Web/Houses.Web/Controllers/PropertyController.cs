using System.Security.Claims;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var properties = await _propertyService.GetAllAsync();

            ViewData["Title"] = "All Properties";

            return View(properties);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddPropertyViewModel
            {
                PropertyTypes = await _propertyService.GetPropertyTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _propertyService.AddPropertyAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid Operation!");

                return View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(string propertyId)
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await _propertyService
                .AddPropertyToCollectionAsync(propertyId, userId!);

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await _propertyService
                .GetMyPropertyAsync(userId!);

            return View("Mine", model);
        }

        public async Task<IActionResult> RemoveFromCollection(string propertyId)
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await _propertyService
                .RemovePropertyFromCollectionAsync(propertyId, userId!);

            return RedirectToAction(nameof(Mine));
        }
    }
}
