using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Houses.Web.Controllers
{
    //[Authorize(Roles = $"{AdministratorRoleName}, {UserRoleName}")]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UserController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProfile()
        {
            try
            {
                string id = User.Id();

                if (id == null)
                {
                    throw new NullReferenceException(
                        string.Format(ExceptionMessages.IdIsNull));
                }

                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    throw new ArgumentException(
                        string.Format(ExceptionMessages.UserNotFound, id));
                }

                await _userManager.DeleteAsync(user);

                return View(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
