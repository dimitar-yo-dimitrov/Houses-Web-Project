using System.Data;
using System.Security.Claims;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.Data.Identity;
using Houses.Infrastructure.GlobalConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertiesTypesService _propertyTypeService;
        private readonly ICityService _cityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyController(
            IPropertyService propertyService,
            IPropertiesTypesService propertyTypeService,
            ICityService cityService,
            UserManager<ApplicationUser> userManager)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _cityService = cityService;
            _userManager = userManager;
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
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var myProperty = await _propertyService
                .GetUserPropertiesAsync(userId!);

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

            ViewData["Title"] = "Add new property";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPropertyViewModel propertyModel)
        {
            ViewData["Title"] = "Add new property";

            if (propertyModel == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, propertyModel?.Id));
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, _userManager.GetUserId(User)));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _propertyService.AddAsync(propertyModel, user.Id);

                    return RedirectToAction(nameof(Mine));
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return View(propertyModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToMyCollection(MyPropertyViewModel model, string? propertyId)
        {
            if (propertyId == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            try
            {
                await _propertyService.AddPropertyToCollectionAsync(propertyId, model.Id);

                return RedirectToAction(nameof(Mine));
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return View(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var property = await _propertyService.GetPropertyAsync(id);

            return (property as IActionResult)!;
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(EditPropertyViewModel propertyToUpdate)
        {
            if (propertyToUpdate == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, propertyToUpdate!.Id));
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, _userManager.GetUserId(User)));
            }

            try
            {
                await _propertyService.EditAsync(propertyToUpdate, user.Id);

                return RedirectToAction(nameof(Mine));
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return View(propertyToUpdate);
        }

        [HttpGet]
        public IActionResult Delete() => RedirectToAction(nameof(All));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string propertyId, string userId)
        {
            await _propertyService.RemovePropertyFromCollectionAsync(propertyId, userId);

            return RedirectToAction(nameof(All));
        }
    }
}
