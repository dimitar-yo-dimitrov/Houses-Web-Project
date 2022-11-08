using System.Security.Claims;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertiesTypesService _propertyTypeService;
        private readonly ICityService _cityService;

        public PropertyController(
            IPropertyService propertyService,
            IPropertiesTypesService propertyTypeService,
            ICityService cityService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _cityService = cityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var properties = await _propertyService.GetAllAsync();

            ViewData["Title"] = "All Properties";

            return View(properties);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            var myProperty = await _propertyService
                .GetMyPropertyAsync(userId);

            return View("Mine", myProperty);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddPropertyViewModel
            {
                PropertyTypes = await _propertyTypeService.GetAllTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
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
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid Operation!");
                model.PropertyTypes = await _propertyTypeService.GetAllTypesAsync();
                model.Cities = await _cityService.GetAllCitiesAsync();

                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string propertyId)
        {
            var targetProperty = await _propertyService.GetMyPropertyAsync(propertyId);

            return View(targetProperty);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddPropertyViewModel model)
        {

            try
            {
                await _propertyService.AddPropertyAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid Operation!");
                model.PropertyTypes = await _propertyTypeService.GetAllTypesAsync();
                model.Cities = await _cityService.GetAllCitiesAsync();
            }

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> AddToCollection(string propertyId)
        {
            var userId = User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            try
            {
                await _propertyService
                    .AddPropertyToMyCollectionAsync(propertyId, userId);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> RemoveFromCollection(string propertyId)
        {
            var userId = User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            await _propertyService
                .RemovePropertyFromCollectionAsync(propertyId, userId!);

            return RedirectToAction(nameof(Mine));
        }
    }
}
