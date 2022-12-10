using Houses.Common.GlobalConstants;
using Houses.Core.Services.Contracts;
using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;
using Houses.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Houses.Common.GlobalConstants.ValidationConstants.AdminConstants;

namespace Houses.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AdminController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetUsers();

            if (users == null)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UsersNotFound));
            }

            return View(users);
        }

        public async Task<IActionResult> Roles(string id)
        {
            if (id == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.IdIsNull));
            }

            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UserNotFound, id));
            }

            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.RoleItems = _roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await _userService.GetUserById(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames.Length > 0)
            {
                await _userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
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

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserInputViewModel model)
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

            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpGet]
        public IActionResult RemoveUser(string userId, string username)
        {
            UserListViewModel model = new UserListViewModel
            {
                Id = userId,
                UserName = username
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(UserListViewModel model)
        {
            try
            {
                if (model.Id == null)
                {
                    throw new NullReferenceException(
                        string.Format(ExceptionMessages.IdIsNull));
                }

                await _userService.DeleteUserAsync(model.Id);

                return RedirectToAction(nameof(ManageUsers));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = AdministratorRoleName
            });

            return Ok();
        }
    }
}
