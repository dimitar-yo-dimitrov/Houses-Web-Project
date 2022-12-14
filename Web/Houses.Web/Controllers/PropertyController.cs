using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.Property;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ExceptionMessages;

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
                queryModel.City,
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.CurrentPage,
                AllPropertyQueryViewModel.HousesPerPage);

            queryModel.TotalHousesCount = result.TotalPropertyCount;
            queryModel.PropertyTypes = await _propertyTypeService.AllPropertyTypeNamesAsync();
            queryModel.Cities = await _cityService.AllCityNamesAsync();
            queryModel.Properties = result.Properties;

            if (queryModel == null)
            {
                throw new NullReferenceException(
                    string.Format(PropertiesNotFound));
            }

            return View(queryModel);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();

            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            IEnumerable<PropertyServiceViewModel> myProperties = await _propertyService.AllPropertiesByUserIdAsync(userId);

            if (myProperties == null)
            {
                throw new NullReferenceException(
                    string.Format(PropertiesNotFound));
            }

            return View(myProperties);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (await _propertyService.ExistAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await _propertyService.PropertyDetailsByIdAsync(id);

            if (model == null)
            {
                throw new ArgumentException(
                    string.Format(PropertyNotFound, id));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new CreatePropertyInputModel
            {
                PropertyTypes = await _propertyTypeService.AllPropertyTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
            };

            ViewData["Title"] = "Add new property";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreatePropertyInputModel propertyModel)
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

            if (userId == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            string id = await _propertyService.CreateAsync(userId, propertyModel);

            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            var property = _propertyService.PropertyDetailsByIdAsync(id);

            if (property == null)
            {
                throw new ArgumentException(
                    string.Format(PropertyNotFound, id));
            }

            var model = new CreatePropertyInputModel
            {
                Title = property.Result.PropertyDto!.Title,
                Price = property.Result.PropertyDto.Price,
                Description = property.Result.PropertyDto.Description,
                Address = property.Result.PropertyDto.Address,
                SquareMeters = property.Result.PropertyDto.SquareMeters,
                ImageUrl = property.Result.PropertyDto.ImageUrl,
                CityId = property.Result.PropertyDto.CityId!,
                PropertyTypeId = property.Result.PropertyDto.PropertyTypeId!,
                PropertyTypes = await _propertyTypeService.AllPropertyTypesAsync(),
                Cities = await _cityService.GetAllCitiesAsync()
            };

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePropertyInputModel propertyToUpdate, string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(IdIsNull));
            }

            if (propertyToUpdate == null)
            {
                throw new ArgumentException(
                    string.Format(PropertyNotFound, id));
            }

            if (!ModelState.IsValid)
            {
                return View(propertyToUpdate);
            }

            await _propertyService.EditAsync(id, propertyToUpdate);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _propertyService.ExistAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var property = await _propertyService.PropertyDetailsByIdAsync(id);

            var model = new DetailsPropertyServiceModel
            {
                Title = property.PropertyDto!.Title,
                Address = property.PropertyDto.Address,
                ImageUrl = property.PropertyDto.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, DetailsPropertyServiceModel model)
        {
            if (await _propertyService.ExistAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            await _propertyService.RemovePropertyFromCollectionAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
