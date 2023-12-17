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

        // GET: Home
        public async Task<IActionResult> Index()
        {
            var viewModel = new SearchViewModel
            {
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Pontuacoes)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Fotografias)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("CategoriaId,OrdemPreco,OrdemRating")] SearchViewModel filter)
        {
            filter.Categorias = await _context.Categorias.ToListAsync();
            filter.Habitacoes = await _context.Habitacoes
                .Include(h => h.Categoria)
                //.Include(h => h.Locador)
                //.Include(h => h.Pontuacoes)
                //.Include(h => h.Tipologia)
                .ToListAsync();

            Console.WriteLine("Categoria: " + filter.CategoriaId);
            Console.WriteLine("Ordem de preco: " + filter.OrdemPreco);
            Console.WriteLine("Ordem de rating: " + filter.OrdemRating);

            return View(filter);
        }

        // GET: Home/Privacy
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