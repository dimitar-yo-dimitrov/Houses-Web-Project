using System.Data;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Infrastructure.GlobalConstants;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertiesTypesService _propertyTypeService;
        private readonly ICityService _cityService;
        private readonly IUserService _userService;

        public PropertyController(
            IPropertyService propertyService,
            IPropertiesTypesService propertyTypeService,
            ICityService cityService,
            IUserService userService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _cityService = cityService;
            _userService = userService;
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
            var userId = User.Id();

            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            IEnumerable<PropertyViewModel> myProperties = await _propertyService.AllPropertiesByUserIdAsync(userId);

            if (myProperties == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertiesNotFound));
            }

            return View(myProperties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var model = new DetailsPropertyViewModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new CreatePropertyViewModel
            {
                PropertyTypes = await _propertyTypeService.GetAllTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
            };

            ViewData["Title"] = "Add new property";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreatePropertyViewModel propertyModel)
        {
            if (await _userService.ExistsById(User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                propertyModel.PropertyTypes = await _propertyTypeService.GetAllTypesAsync();
                propertyModel.Cities = await _cityService.GetAllCitiesAsync();

                return View(propertyModel);
            }

            string userId = await _userService.GetUserId(User.Id());

            string id = await _propertyService.CreateAsync(propertyModel, userId);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToMyCollection(PropertyViewModel model, string? propertyId)
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
        public async Task<IActionResult> Edit(EditPropertyViewModel propertyToUpdate, string id)
        {
            if (propertyToUpdate == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, propertyToUpdate!.Id));
            }

            try
            {
                //await _propertyService.EditAsync(propertyToUpdate, user.Id);

                return RedirectToAction(nameof(Mine));
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidOperation);
            }

            return View(propertyToUpdate);
        }

        //[HttpGet]
        //public IActionResult Delete() => RedirectToAction(nameof(All));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            //Guid idGuid = Guid.Parse(id);
            await _propertyService.RemovePropertyFromCollectionAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
