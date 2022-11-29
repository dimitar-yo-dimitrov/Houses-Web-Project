using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    //[Authorize(Roles = $"{AdministratorRoleName}, {UserRoleName}")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPropertyService _propertyService;

        public UserController(
            IUserService userService,
            RoleManager<IdentityRole> roleManager, IPropertyService propertyService)
        {
            _userService = userService;
            _roleManager = roleManager;
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var userId = User.Id();

            if (userId == null)
            {
                throw new NullReferenceException(
                string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _userService.GetUserByIdForProfile(userId);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, userId));
            };

            return View(user);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UserProfile([FromQuery] string name)
        {
            var user = await _userService.GetUserByIdForProfile(name);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return View(user as UserServiceViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string? id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.UserNotFound, id));
            }

            var model = new EditUserInputViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var inputModel = new EditUserInputViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfilePicture = model.ProfilePicture,
            };

            if (await _userService.UpdateUser(inputModel))
            {
                ViewData[ExceptionMessages.SuccessMessage] = ExceptionMessages.SuccessfulRecord;
            }
            else
            {
                ViewData[ExceptionMessages.ErrorMessage] = ExceptionMessages.InvalidOperation;
            }

            return View(model);
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = AdministratorRoleName
        //    });

        //    return Ok();
        //}
    }
}
