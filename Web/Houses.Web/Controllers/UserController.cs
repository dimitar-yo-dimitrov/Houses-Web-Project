using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.Role;

namespace Houses.Web.Controllers
{
    [Authorize(Roles = $"{AdministratorRoleName}, {UserRoleName}")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            IUserService userService,
            RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
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

        public async Task<IActionResult> CreateRole()
        {
            //await _roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = AdministratorRoleName
            //});

            return Ok();
        }
    }
}
