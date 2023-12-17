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
            var categorias = await _context.Categorias
                .Select(c => new { Id = c.Id, Nome = c.Nome })
                .Distinct()
                .ToListAsync();
            categorias.Insert(0, new { Id = 0, Nome = "Todas" });
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            var locadores = await _context.Locadores
                .Select(l => new { Id = l.Id, Nome = l.Nome })
                .Distinct()
                .ToListAsync();
            locadores.Insert(0, new { Id = 0, Nome = "Todos" });
            ViewBag.Locadores = new SelectList(locadores, "Id", "Nome");

            var habitacoes = await _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador)
                .Include(h => h.Tipologia)
                .Include(h => h.Fotografias)
                .ToListAsync();

            foreach (var habitacao in habitacoes)
            {
                habitacao.Alugueres = await _context.Alugueres
                    .Include(a => a.Pontuacao)
                    .Where(a => a.HabitacaoId == habitacao.Id)
                    .ToListAsync();
            }

            var viewModel = new SearchViewModel
            {
                Habitacoes = habitacoes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("CategoriaId,LocadorId,OrdemPreco,OrdemRating")] SearchViewModel filter)
        {
            var categorias = await _context.Categorias
                .Select(c => new { Id = c.Id, Nome = c.Nome })
                .Distinct()
                .ToListAsync();
            categorias.Insert(0, new { Id = 0, Nome = "Todas" });
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            var locadores = await _context.Locadores
                .Select(l => new { Id = l.Id, Nome = l.Nome })
                .Distinct()
                .ToListAsync();
            locadores.Insert(0, new { Id = 0, Nome = "Todos" });
            ViewBag.Locadores = new SelectList(locadores, "Id", "Nome");

            var habitacoesQuery = _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador)
                .Include(h => h.Tipologia)
                .Include(h => h.Fotografias)
                .AsQueryable();

            if (filter.CategoriaId != 0 && filter.CategoriaId != null)
                habitacoesQuery = habitacoesQuery.Where(h => h.CategoriaId == filter.CategoriaId);

            if (filter.LocadorId != 0 && filter.LocadorId != null)
                habitacoesQuery = habitacoesQuery.Where(h => h.LocadorId == filter.LocadorId);

            if (filter.OrdemPreco == "Ascendente")
                habitacoesQuery = habitacoesQuery.OrderBy(h => h.CustoPorNoite);
            else if (filter.OrdemPreco == "Descendente")
                habitacoesQuery = habitacoesQuery.OrderByDescending(h => h.CustoPorNoite);

            //TODO: quick check
            /*if (filter.OrdemRating == "Ascendente")
                habitacoesQuery = habitacoesQuery.OrderBy(h => h.mediaPontuacoes);
            else if (filter.OrdemRating == "Descendente")
                habitacoesQuery = habitacoesQuery.OrderByDescending(h => h.mediaPontuacoes);*/

            filter.Habitacoes = await habitacoesQuery.ToListAsync();

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