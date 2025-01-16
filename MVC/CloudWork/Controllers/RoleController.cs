﻿using CloudWork.Model;
using CloudWork.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudWork.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(role.Name ?? string.Empty);
                if (roleExists)
                {
                    ModelState.AddModelError("", "Role已存在");
                    return View(role);
                }

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("Error");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                UserNames = new List<string>()
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name ?? string.Empty))
                {
                    model.UserNames.Add(user.UserName ?? string.Empty);
                }
            }

            return View(model);
            // return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var _role = await _roleManager.FindByIdAsync(role.Id);
                if (_role == null)
                {
                    ModelState.AddModelError(role.Id, "找不到");
                    return View("NotFound");
                }
                
                _role.Name = role.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(_role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        [HttpPost]
        [Authorize(Policy = "ChangeRole")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"找不到Id为 {id} 的Role";
                return View("NotFound");
            }

            try
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index", await _roleManager.Roles.ToListAsync());
            }
            catch (DbUpdateException ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorTitle = $"{role.Name} Role 已被使用";
                ViewBag.ErrorMessage = $"{role.Name} 不能被删除，需要先移除其中的所有账户";
                return View("Error");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.roleId = id;

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"找不到Id为 {id} 的Role";
                return View("NotFound");
            }

            ViewBag.RollName = role.Name;
            var model = new List<UsersForRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                bool isSelected = await _userManager.IsInRoleAsync(user, role.Name);
                var userRoleViewModel = new UsersForRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = isSelected
                };

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UsersForRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"找不到Id为 {id} 的Role";
                return View("NotFound");
            }

            foreach (var userRole in model)
            {
                var user = await _userManager.FindByIdAsync(userRole.UserId);

                if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Edit", new { id });
        }
    }
}
