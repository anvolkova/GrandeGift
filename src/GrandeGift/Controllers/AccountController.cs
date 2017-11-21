using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;


namespace GrandeGift.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signinManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Profile> _profileDataService;
        
        public AccountController(UserManager<IdentityUser> userManagerService,
            SignInManager<IdentityUser> signinManagerService,
            RoleManager<IdentityRole> roleManagerService,
            IDataService<Profile> profileService)
        {
            _userManagerService = userManagerService;
            _signinManagerService = signinManagerService;
            _roleManagerService = roleManagerService;
            _profileDataService = profileService;
        }
        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser(vm.Username);
                user.Email = vm.Email;                
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _userManagerService.AddToRoleAsync(user, "Customer");
                    Profile newProfile = new Profile { Username = vm.Username };
                    _profileDataService.Create(newProfile);                   
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }

        //Login
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "User name or Password is incorrect");
            return View(vm);
        }

        //Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signinManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Update profile
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateProfile()
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            //if there is a profile data:
            Profile profile = _profileDataService.GetSingle(p => p.Username == user.UserName);
            AccountUpdateProfileViewModel vm = new AccountUpdateProfileViewModel
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = user.Email,
                Phone = profile.Phone
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateProfile(AccountUpdateProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Profile p = new Profile
                {
                    ProfileId = vm.ProfileId,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Phone = vm.Phone,                    
                    Username = User.Identity.Name
                };
                _profileDataService.Update(p);
                //update email
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                user.Email = vm.Email;
                await _userManagerService.UpdateAsync(user);

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
    }
}
