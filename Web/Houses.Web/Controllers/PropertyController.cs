using System.Security.Claims;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Constants;
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
                .UserPropertiesAsync(userId);

            return View(myProperty);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddPropertyViewModel
            {
                PropertyTypes = await _propertyTypeService.GetAllTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
            };

            ViewData["Title"] = "Add new property";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyViewModel model, string userId)
        {
            ViewData["Title"] = "Add new property";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _propertyService.AddPropertyAsync(model, userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
                model.PropertyTypes = await _propertyTypeService.GetAllTypesAsync();
                model.Cities = await _cityService.GetAllCitiesAsync();

                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyCollection(MyPropertyViewModel model, string propertyId)
        {
            try
            {
                await _propertyService.AddPropertyToCollectionAsync(propertyId, model.Id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string propertyId)
        {
            var property = await _propertyService.GetPropertyByIdAsync<MyPropertyViewModel>(propertyId);

            return (property as IActionResult)!;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPropertyViewModel model, string userId)
        {
            try
            {
                await _propertyService.EditAsync(model, userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> RemoveFromCollection(string propertyId)
        {
            var userId = User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            await _propertyService
                .RemovePropertyFromCollectionAsync(propertyId, userId);

            return RedirectToAction(nameof(Mine));
        }
    }
}
