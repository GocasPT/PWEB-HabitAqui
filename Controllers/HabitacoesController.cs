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

namespace HabitAqui.Controllers
{
    //[Authorize(Roles = "Funcionario")] TIRAR ISTO DEPOIS (NAO TEMOS FUNCIONARIOS)
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
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Nome");
            return View();
        }

        // POST: Habitacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descricao,CategoriaId,Tipologia,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,LocadorId")] Habitacao habitacao)
        {
            if (ModelState.IsValid)
            {
                habitacao.DataCriacao = DateTime.UtcNow;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descricao,CategoriaId,Tipologia,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,LocadorId")] Habitacao habitacao)
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
        public async Task<IActionResult> Search(int? categoriaId, string searchString, string orderPrice, string orderRating)
        {
            var viewModel = new HomeSearchViewModel
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
    }
}
