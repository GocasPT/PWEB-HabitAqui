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

namespace HabitAqui.Controllers
{
    public class HabitacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HabitacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Habitacoes
        public async Task<IActionResult> Index()
        {
            var habitacoes = _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador);

            return View(await habitacoes.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacao == null)
            {
                return NotFound();
            }

            var habitacaoItens = await _context.Habitacoes_Itens
            .Where(hi => hi.HabitacaoId == habitacao.Id)
            .Include(hi => hi.Item)
            .ToListAsync();

            ViewBag.HabitacaoItens = habitacaoItens;

            return View(habitacao);
        }

        // GET: Habitacoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome");
            return View();
        }

        // POST: Habitacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoriaId,Tipologia,Pais,Distrito,Concelho,Rua,CustoPorNoite,Disponivel,LocadorId")] Habitacao habitacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome", habitacao.LocadorId);

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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome", habitacao.LocadorId);
            return View(habitacao);
        }

        // POST: Habitacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoriaId,Tipologia,Pais,Distrito,Concelho,Rua,CustoPorNoite,Disponivel,LocadorId")] Habitacao habitacao)
        {
            if (id != habitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacao);
                    await _context.SaveChangesAsync();
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome", habitacao.LocadorId);
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
            var habitacao = await _context.Habitacoes.FindAsync(id);
            if (habitacao != null)
            {
                _context.Habitacoes.Remove(habitacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Habitacoes/Search
        public async Task<IActionResult> Search(string categoria)
        {
            if (categoria == "All")
                categoria = null;

            //ViewData["TextoAPesquisar"] = HttpContext.Request.Query["texto"];
            var textoAPesquisar = TempData["TextoAPesquisar"].ToString();

            var viewModelFilter = new HomeSearchViewModel
            {
                TextoAPesquisar = textoAPesquisar,
                CategoriaFilter = categoria,
                Categorias = await _context.Categorias.ToListAsync(),
                Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Where(h => h.Name.Contains(textoAPesquisar))
                    .ToListAsync()
            };

            if (categoria != null)
                viewModelFilter.Habitacoes = viewModelFilter.Habitacoes.Where(h => h.Categoria.Nome.Equals(categoria)).ToList();

            viewModelFilter.NumResultados = viewModelFilter.Habitacoes.Count();

            TempData.Keep("TextoAPesquisar");

            return View(viewModelFilter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("TextoAPesquisar")] HomeSearchViewModel pesquisaHabitacao)
        {
            var habitacoesQuery = _context.Habitacoes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisaHabitacao.TextoAPesquisar))
            {
                habitacoesQuery = habitacoesQuery
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Where(h => h.Name.Contains(pesquisaHabitacao.TextoAPesquisar))
                    .OrderBy(h => h.Name);
            }
            else
            {
                habitacoesQuery = habitacoesQuery
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .OrderBy(h => h.Name);
            }

            pesquisaHabitacao.Categorias = await _context.Categorias.ToListAsync();
            pesquisaHabitacao.Habitacoes = await habitacoesQuery.ToListAsync();
            pesquisaHabitacao.NumResultados = pesquisaHabitacao.Habitacoes.Count();

            TempData.Keep("TextoAPesquisar");

            return View(pesquisaHabitacao);
        }

        private bool HabitacaoExists(int id)
        {
            return (_context.Habitacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
