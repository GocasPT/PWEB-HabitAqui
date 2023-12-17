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
        public async Task<IActionResult> Index(int? categoriaId, string orderPrice, string orderRating)
        {
            var viewModel = new HomeViewModel
            {
                OrdemPreco = orderPrice,
                OrdemRating = orderRating,
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Pontuacoes)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Fotografias)
                    .ToListAsync()
            };

            if (categoriaId != null)
                viewModel.Habitacoes = viewModel.Habitacoes.Where(h => h.Categoria.Id == categoriaId).ToList();

            //TODO: fazer o filtro
            //if (orderPrice != null)
            //    viewModel.Habitacoes = viewModel.Habitacoes.OrderBy(h => h.CustoPorNoite).ToList();

            //if (orderRating != null)
            //    viewModel.Habitacoes = viewModel.Habitacoes.OrderBy(h => h.Pontuacoes).ToList();

            return View(viewModel);
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