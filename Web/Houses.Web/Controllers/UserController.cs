using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ExceptionMessages;

namespace Houses.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(
            IUserService userService,
            ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            try
            {
                var userId = User.Id();

                _logger.LogInformation("User with {id}{name} at {RequestTime}", "ARG0", userId, DateTime.Now);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new NullReferenceException(
                        string.Format(IdIsNull));
                }

                var user = await _userService.GetUserByIdForProfile(userId);

                if (user == null)
                {
                    throw new NullReferenceException(
                        string.Format(UserNotFound, userId));
                }

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong: {ex}", nameof(MyProfile));

                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                string id = User.Id();

                _logger.LogInformation("Get user with {id}{name} at {RequestTime}", "ARG0", id, DateTime.Now);

                if (string.IsNullOrEmpty(id))
                {
                    throw new NullReferenceException(
                        string.Format(IdIsNull));
                }

                var user = await _userService.GetUserForEdit(id);

                if (user == null)
                {
                    throw new ArgumentException(
                        string.Format(UserNotFound, id));
                }

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong: {ex}", nameof(EditProfile));

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserInputViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (await _userService.UpdateUser(model))
                {
                    _logger.LogInformation("User with {model.Id}{model.FirstName} at {RequestTime} is updated", "ARG0", model, DateTime.Now);

                    ViewData[SuccessMessage] = SuccessfulRecord;
                }
                else
                {
                    ViewData[ErrorMessage] = InvalidOperation;
                }

                return RedirectToAction(nameof(MyProfile));
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong: {ex}", nameof(EditProfile));

                return NotFound(ex.Message);
            }
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
