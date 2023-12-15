using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;
using HabitAqui.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using HabitAqui.Migrations;
using System.Diagnostics;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Funcionario, Gestor")]
    public class HabitacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HabitacoesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Habitacoes
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var habitacoes = _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Alugueres)
                    .Where(h => h.LocadorId == currentUser.LocadorId);

                return View(await habitacoes.ToListAsync());
            }
            else
            {
                var habitacoes = _context.Habitacoes
                      .Include(h => h.Categoria)
                      .Include(h => h.Locador)
                      .Include(h => h.Tipologia);
                return View(await habitacoes.ToListAsync());
            }
        }

        // GET: Habitacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador)
                .Include(h => h.Tipologia)
                .Include(h => h.Fotografias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacao == null)
            {
                return NotFound();
            }

            var habitacaoItens = await _context.Habitacoes_Itens
            .Where(hi => hi.HabitacaoId == habitacao.Id)
            .Include(hi => hi.Item)
            .ToListAsync();

            habitacao.Pontuacoes = await _context.Pontuacoes
            .Where(p => p.HabitacaoId == habitacao.Id)
            .ToListAsync();

            ViewBag.HabitacaoItens = habitacaoItens;

            return View(habitacao);
        }

        // GET: Habitacoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["TipologiaId"] = new SelectList(_context.Tipologia, "Id", "Nome");
            //ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome");
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View();
        }

        // POST: Habitacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descricao,CategoriaId,TipologiaId,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,Latitude,Longitude,Itens,Fotografias")] Habitacao habitacao, List<IFormFile> Fotografias)
        {
            //TODO: receber a(s) foto(s)
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                habitacao.DataCriacao = DateTime.UtcNow;
                habitacao.LocadorId = currentUser.LocadorId;
                _context.Add(habitacao);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(Request.Form["Itens"]))
                {
                    var selectedItens = Request.Form["Itens"].ToString().Split(',').Select(int.Parse).ToArray();

                    if (selectedItens.Length > 0)
                    {
                        foreach (var itemId in selectedItens)
                        {
                            var habitacaoItem = new Habitacao_Itens
                            {
                                HabitacaoId = habitacao.Id,
                                ItemId = itemId
                            };
                            _context.Add(habitacaoItem);
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "habitacoes");

                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                foreach (var formFile in Fotografias)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                        var filePath = Path.Combine(uploadsPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(fileStream);
                        }

                        var fotografia = new Models.Fotografia
                        {
                            Nome = fileName,
                            Extensao = Path.GetExtension(fileName),
                            HabitacaoId = habitacao.Id
                        };

                        _context.Add(fotografia);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
            ViewData["TipologiaId"] = new SelectList(_context.Tipologia, "Id", "Nome", habitacao.TipologiaId);
            //ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome", habitacao.LocadorId);
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Name", habitacao.Itens);

            return View(habitacao);
        }

        // GET: Habitacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes.FindAsync(id);
            if (habitacao == null)
            {
                return NotFound();
            }
            ViewData["LocadorNome"] = _context.Locadores.Find(habitacao.LocadorId)?.Nome;
            ViewData["TipologiaNome"] = _context.Tipologia.Find(habitacao.TipologiaId)?.Nome;
            ViewData["CategoriaNome"] = _context.Categorias.Find(habitacao.CategoriaId)?.Nome;

            var selectedItensID = _context.Habitacoes_Itens
                .Where(hi => hi.HabitacaoId == habitacao.Id)
                .Select(hi => hi.Item.Id)
                .ToList();

            ViewBag.Itens = new MultiSelectList(_context.Itens, "Id", "Description", selectedItensID);
            return View(habitacao);
        }

        // POST: Habitacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descricao,CategoriaId,TipologiaId,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,LocadorId,Latitude,Longitude,Itens")] Habitacao habitacao)
        {
            if (id != habitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    habitacao.DataCriacao = _context.Habitacoes.AsNoTracking().FirstOrDefault(h => h.Id == id)?.DataCriacao ?? DateTime.UtcNow;
                    _context.Update(habitacao);
                    await _context.SaveChangesAsync();

                    _context.Habitacoes_Itens.RemoveRange(_context.Habitacoes_Itens.Where(hi => hi.HabitacaoId == habitacao.Id));
                    await _context.SaveChangesAsync();

                    var selectedItens = Request.Form["Itens"].ToString().Split(',').Select(int.Parse).ToArray();
                    if (selectedItens.Length > 0)
                    {
                        foreach (var itemId in selectedItens)
                        {
                            var habitacaoItem = new Habitacao_Itens
                            {
                                HabitacaoId = habitacao.Id,
                                ItemId = itemId
                            };
                            _context.Add(habitacaoItem);
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacaoExists(habitacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocadorNome"] = _context.Locadores.Find(habitacao.LocadorId)?.Nome;
            ViewData["TipologiaNome"] = _context.Tipologia.Find(habitacao.TipologiaId)?.Nome;
            ViewData["CategoriaNome"] = _context.Categorias.Find(habitacao.CategoriaId)?.Nome;
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description", habitacao.Itens);
            return View(habitacao);
        }

        // GET: Habitacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador)
                .Include(h => h.Tipologia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacao == null)
            {
                return NotFound();
            }

            return View(habitacao);
        }

        // POST: Habitacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habitacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Habitacoes'  is null.");
            }
            var habitacao = await _context.Habitacoes
                .Include(h => h.Fotografias)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (habitacao != null)
            {
                _context.Habitacoes.Remove(habitacao);
            }

            foreach (var fotografia in habitacao.Fotografias)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/habitacoes", fotografia.Nome);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Habitacoes/Search
        [AllowAnonymous]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Search(int? categoriaId, string searchString, string orderPrice, string orderRating)
        {
            var viewModel = new SearchViewModel
            {
                TextoAPesquisar = searchString,
                OrdemPreco = orderPrice,
                OrdemRating = orderRating,
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Where(h => h.Name.Contains(searchString))
                    .ToListAsync()
            };

            if (categoriaId != null)
                viewModel.Habitacoes = viewModel.Habitacoes.Where(h => h.Categoria.Id == categoriaId).ToList();

            //TODO: fazer o filtro
            //if (orderPrice != null)
            //    viewModel.Habitacoes = viewModel.Habitacoes.OrderBy(h => h.CustoPorNoite).ToList();

            //if (orderRating != null)
            //    viewModel.Habitacoes = viewModel.Habitacoes.OrderBy(h => h.Pontuacoes).ToList();

            viewModel.NumResultados = viewModel.Habitacoes.Count();

            return View(viewModel);
        }

        private bool HabitacaoExists(int id)
        {
            return (_context.Habitacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Disable(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes.FindAsync(id);
            if (habitacao == null)
            {
                return NotFound();
            }

            habitacao.Disponivel = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes.FindAsync(id);
            if (habitacao == null)
            {
                return NotFound();
            }

            habitacao.Disponivel = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
