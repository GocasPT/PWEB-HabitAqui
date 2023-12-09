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
            var viewModel = new HomeIndexViewModel
            {
                CategoriaFilter = null,
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Home
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("CategoriaFilter")] HomeIndexViewModel filter)
        {
            ModelState.Remove(nameof(filter.Categorias));
            ModelState.Remove(nameof(filter.Habitacoes));

            if (ModelState.IsValid)
            {
                var categoria = await _context.Categorias.ToListAsync();

                var habitacao = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .ToListAsync();

                if (filter.CategoriaFilter != null)
                    habitacao = habitacao.Where(h => h.Categoria.Nome.Equals(filter.CategoriaFilter)).ToList();

                filter.Categorias = categoria;
                filter.Habitacoes = habitacao;
            }

            return View(filter);
        } 

        //GET: Home/Search
        public async Task<IActionResult> Search()
        {
            var viewModel = new HomeSearchViewModel
            {
                CategoriaFilter = null,
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Home/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("Rua,CheckIn,CheckOut,CategoriaFilter")] HomeSearchViewModel search)
        {
            //TODO: validar datas + mensagem de erro em popup
            //if (search.CheckIn.CompareTo(search.CheckOut) > 0)
            //    return Problem("Data de check-in não pode ser superior à data de check-out");
            ModelState.Remove(nameof(search.CategoriaFilter));
            ModelState.Remove(nameof(search.Categorias));
            ModelState.Remove(nameof(search.Habitacoes));

            if (ModelState.IsValid)
            {
                var categorias = await _context.Categorias.ToListAsync();

                //TODO: Filtrar habitacoes com datas disponiveis           
                var habitacao = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Where(h => h.Rua.Contains(search.Rua))
                    .ToListAsync();

                if (search.CategoriaFilter != null)
                    habitacao = habitacao.Where(h => h.Categoria.Nome.Equals(search.CategoriaFilter)).ToList();

                search.Categorias = categorias;
                search.Habitacoes = habitacao;
            }

            return View(search);
        }

        // GET Home/Privacy
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