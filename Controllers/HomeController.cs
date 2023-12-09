using HabitAqui.Data;
using HabitAqui.Models;
using HabitAqui.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HabitAqui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var habits = _context.Habitacoes
                .Select(h => h.Rua)
                .Distinct()
                .ToList();
            ViewData["Habitacoes"] = new SelectList(habits);

            var categorias = _context.Categorias
                .Select(c => c.Nome)
                .Distinct()
                .ToList();
            ViewData["Categorias"] = new SelectList(categorias);

            return View(_context.Habitacoes);
        }

        //TODO: Completar a função Details
        public IActionResult Details()
        {
            return View();
        }

        // POST: Habitacoes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("Rua,CheckIn,CheckOut")] HabitacaoSearchViewModel search)
        {
            if (search.CheckIn.CompareTo(search.CheckOut) > 0)
                return Problem("Data de check-in não pode ser superior à data de check-out");

            ModelState.Remove(nameof(search.Categoria));
            ModelState.Remove(nameof(search.Habitacoes));

            if (ModelState.IsValid)
            {
                var habitacao = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Where(h => h.Rua.Contains(search.Rua))
                    .ToListAsync();

                search.Categoria = habitacao.Select(h => h.Categoria.Nome).FirstOrDefault();
                search.Habitacoes = habitacao;
            }

            return View("Search", search);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}