using HabitAqui.Data;
using HabitAqui.Models;
using HabitAqui.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserManagerController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserManagerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        //var currectUser = await _userManager.GetUserAsync(User);
        

        var viewModelList = new List<UserManagerViewModel>();
        var users = await _userManager.Users.ToListAsync();

        foreach (var user in users)
        {
            var roles = await GetUserRoles(user);
            var viewModel = new UserManagerViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Ativo = user.Ativo,
                Roles = roles
            };

            viewModelList.Add(viewModel);
        }

        return View(viewModelList);
    }

    private async Task<List<string>> GetUserRoles(ApplicationUser user)
    {
        return new List<string>(await _userManager.GetRolesAsync(user));
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

        user.Ativo = false;
        await _userManager.UpdateAsync(user);

        return RedirectToAction(nameof(Index));
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

        user.Ativo = true;
        await _userManager.UpdateAsync(user);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Funcionarios()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var users = await _userManager.Users
            .Where(u => u.LocadorId == currentUser.LocadorId)
            .ToListAsync();

        ViewBag.LocadorId = currentUser.LocadorId;

        var viewModelList = new List<UserManagerViewModel>();

        foreach (var user in users)
        {
            if (user.Id == currentUser?.Id)
            {
                continue;
            }

            var roles = await GetUserRoles(user);
            if (roles.Contains("Administrador"))
            {
                continue;
            }

            var checkins = await _context.CheckIns
                .Where(c => c.FuncionarioId == user.Id)
                .ToListAsync();

            var checkouts = await _context.CheckOuts
                .Where(c => c.FuncionarioId == user.Id)
                .ToListAsync();

            var viewModel = new UserManagerViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Ativo = user.Ativo,
                Roles = roles,
                CheckIns = checkins,
                CheckOuts = checkouts
            };

            viewModelList.Add(viewModel);
        }

        return View("Index", viewModelList);
    }
}