﻿using Houses.Common.GlobalConstants;
using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.ClaimsConstants;

namespace Houses.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Property");
            }

            var model = new RegisterInputModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                UserName = model.Email,
                ProfilePicture = model.ProfilePicture

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager
                .AddClaimAsync(user, new System.Security.Claims.Claim(FirstName, user.FirstName ?? user.Email));

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                ////create a claim
                //var claimEmail = new Claim(ClaimTypes.Email, user.Email);
                //var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id);

                ////create claimsIdentity
                //var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimIdentifier }, "serverAuth");

                ////create claimsPrincipal
                //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                ////Sign In User
                //await HttpContext.SignInAsync(claimsPrincipal);

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.InvalidLogin);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
