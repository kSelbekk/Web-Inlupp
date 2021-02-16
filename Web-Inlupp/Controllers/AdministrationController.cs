using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            ApplicationDbContext dbContext) : base(dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            if (!ModelState.IsValid) return RedirectToAction("EditRole", new { Id = roleId });

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (var i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result;

                if (model[i].IsSelected && !await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (!result.Succeeded) continue;
                if (i < model.Count - 1)
                    continue;
                return RedirectToAction("EditRole", new { Id = roleId });
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(ProjectRoleViewModel role)
        //{
        //    if (!ModelState.IsValid) return View();

        //    IdentityRole identityRole = new IdentityRole
        //    {
        //        Name = role.RoleName
        //    };

        //    IdentityResult result = await _roleManager.CreateAsync(identityRole);

        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ListRoles", "Administration");
        //    }

        //    foreach (IdentityError error in result.Errors)
        //    {
        //        ModelState.AddModelError("RoleName", error.Description);
        //    }

        //    return View();
        //}

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult ListUser()
        {
            var viewModel = new ListUserViewModel();
            viewModel.Users = DbContext.Users
                .Select(u => new ListUserViewModel.ListUser
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName
                }).ToList();

            return View(viewModel);
        }

        public IActionResult EditUser(string id)
        {
            var viewModel = new EditUserViewModel();
            var dbUser = DbContext.Users
                .First(i => i.Id == id);

            viewModel.UserName = dbUser.UserName;
            viewModel.Email = dbUser.Email;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel viewModel, string id)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var dbUser = DbContext.Users.FirstOrDefault(i => i.Id == id);
            if (dbUser == null)
            {
                return RedirectToAction("ListUser");
            }
            dbUser.UserName = viewModel.UserName;
            dbUser.Email = viewModel.Email;

            if (viewModel.Password == null)
            {
                DbContext.SaveChanges();
                return RedirectToAction("ListRoles");
            }

            var resetToken = _userManager.GeneratePasswordResetTokenAsync(dbUser).Result;
            var user = _userManager.ResetPasswordAsync(dbUser, resetToken, viewModel.Password).Result;
            if (!user.Succeeded)
            {
                ModelState.AddModelError("Password", "You need a better password");
                return View(viewModel);
            }
            DbContext.SaveChanges();
            return RedirectToAction("ListRoles");
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            if (!ModelState.IsValid) return RedirectToAction("ListUser");

            var users = _userManager.GetUsersInRoleAsync("Admin").Result;

            var dbUser = DbContext.Users.First(db => db.Id == id);

            if (_userManager.IsInRoleAsync(dbUser, "Admin").Result && users.Count < 2)
            {
                ViewBag.ErrorMessage = "Error! There can't be zero Admins";
                return View("DeleteUserError");
            }

            DbContext.Users.Remove(dbUser);
            DbContext.SaveChanges();
            return RedirectToAction("ListUser");
        }

        public IActionResult NewUser()
        {
            var viewModel = new NewUserIndexViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewUser(NewUserIndexViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (DbContext.Users.Any(dbUser => dbUser.Email == viewModel.Email))
            {
                ModelState.AddModelError("Email", "User already exist");
                return View(viewModel);
            }

            var dbUser = new IdentityUser();
            DbContext.AddAsync(dbUser);

            dbUser.UserName = viewModel.UserName;
            dbUser.Email = viewModel.Email;
            dbUser.EmailConfirmed = true;
            dbUser.NormalizedEmail = viewModel.Email.ToUpper();

            var hasher = new PasswordHasher<IdentityUser>();
            dbUser.PasswordHash = hasher.HashPassword(dbUser, viewModel.Password);

            DbContext.SaveChanges();

            return RedirectToAction("ListRoles");
        }
    }
}