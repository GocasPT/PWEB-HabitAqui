using HabitAqui.Data;
using HabitAqui.Models;
using HabitAqui.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserRolesManagerController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModelList = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await GetUserRoles(user);

                var userRolesViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    PrimeiroNome = user.PrimeiroNome,
                    UltimoNome = user.UltimoNome,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                };

                userRolesViewModelList.Add(userRolesViewModel);
            }

            return View(userRolesViewModelList);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> Details(string userId)
        {
            if (userId == null || _userManager.Users == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();

            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in roles)
            {
                ManageUserRolesViewModel manage = new ManageUserRolesViewModel();
                manage.RoleId = role.Id;
                manage.RoleName = role.Name;
                manage.Selected = await _userManager.IsInRoleAsync(user, role.Name);
                model.Add(manage);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                foreach (var role in model)
                {
                    if (role.Selected)
                    {
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Disable(string userId)
        {
            if (userId == null || _userManager.Users == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //user.Ativo = false;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Enable(string userId)
        {
            if (userId == null || _userManager.Users == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //user.Ativo = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
    }
}