using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.Role;

namespace Houses.Web.Controllers
{
    [Authorize(Roles = $"{AdministratorRoleName}, {UserRoleName}")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var model = await _userService.GetUserByName(User.Identity!.Name!);

            if (model == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UserProfile([FromQuery] string name)
        {
            var model = await _userService.GetUserByName(name);

            if (model == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var model = await _userService.GetUserByName(User.Identity!.Name!);

            if (model == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserProfileInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var inputModel = new EditUserProfileInputModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
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
    }
}
