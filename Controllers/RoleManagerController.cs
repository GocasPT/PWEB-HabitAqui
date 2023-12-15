using HabitAqui.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RoleManagerController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var role = new IdentityRole(roleName);

            return RedirectToAction("Index");
        }

    }
}
