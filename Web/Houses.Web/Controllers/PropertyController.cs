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
        public async Task<IActionResult> All([FromQuery] AllPropertyQueryViewModel queryModel)
        {
            var result = await _propertyService.GetAllAsync(
                queryModel.PropertyType,
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.CurrentPage,
                AllPropertyQueryViewModel.HousesPerPage);

            queryModel.TotalHousesCount = result.TotalPropertyCount;
            queryModel.PropertyTypes = await _propertyTypeService.AllPropertyTypeNamesAsync();
            queryModel.Properties = result.Properties;

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
                PropertyTypes = await _propertyTypeService.AllPropertyTypesAsync(),
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
                propertyModel.PropertyTypes = await _propertyTypeService.AllPropertyTypesAsync();
                propertyModel.Cities = await _cityService.GetAllCitiesAsync();

                return View(propertyModel);
            }

            string userId = await _userService.GetUserId(User.Id());

            string id = await _propertyService.CreateAsync(propertyModel, userId);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var property = await _propertyService.GetPropertyAsync(id);

            if (property == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, id));
            }

            var model = new CreatePropertyViewModel
            {
                Title = property.Title,
                Price = property.Price,
                Description = property.Description,
                Address = property.Address,
                SquareMeters = property.SquareMeters,
                ImageUrl = property.ImageUrl,
                CityId = property.CityId,
                PropertyTypeId = property.PropertyTypeId,
                PropertyTypes = await _propertyTypeService.AllPropertyTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
            };

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePropertyViewModel propertyToUpdate, string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            if (propertyToUpdate == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PropertyNotFound, propertyToUpdate!.Id));
            }

            if (!ModelState.IsValid)
            {
                return View(propertyToUpdate);
            }

            await _propertyService.EditAsync(propertyToUpdate, id);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            await _propertyService.RemovePropertyFromCollectionAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
