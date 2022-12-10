using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
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
            try
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                string id = User.Id();

                if (id == null)
                {
                    throw new NullReferenceException(
                        string.Format(ExceptionMessages.IdIsNull));
                }

                var user = await _userService.GetUserForEdit(id);

                if (user == null)
                {
                    throw new ArgumentException(
                        string.Format(ExceptionMessages.UserNotFound, id));
                }

                return View(user);
            }
            catch (Exception ex)
            {
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
                    ViewData[ExceptionMessages.SuccessMessage] = ExceptionMessages.SuccessfulRecord;
                }
                else
                {
                    ViewData[ExceptionMessages.ErrorMessage] = ExceptionMessages.InvalidOperation;
                }

                return RedirectToAction(nameof(MyProfile));
            }
            catch (Exception ex)
            {
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
