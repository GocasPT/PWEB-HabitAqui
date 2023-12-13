using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;
using System.Security.Claims;
using System.Diagnostics;

namespace HabitAqui.Controllers
{
    public class PontuacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PontuacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pontuacoes
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Cliente"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pontuacoes = await _context.Pontuacoes
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Habitacao)
                    .Where(p => p.ApplicationUserId == currentUserId)
                    .ToListAsync();
                return View(pontuacoes);
            }

            var applicationDbContext = _context.Pontuacoes.Include(p => p.ApplicationUser).Include(p => p.Habitacao);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pontuacoes/Create
        public IActionResult Create(int habitacaoId)
        {
            ViewBag.HabitacaoId = habitacaoId;
            return View();
        }

        // POST: Pontuacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Comentario,PontuacaoLimpeza,PontuacaoLocalizacao,PontuacaoQualidadePreco,PontuacaoEspaco,HabitacaoId")] Pontuacao pontuacao)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                pontuacao.ApplicationUserId = userId;
                
                pontuacao.DataCriacao = DateTime.UtcNow;
                _context.Add(pontuacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Alugueres");
            }

            return View(pontuacao);
        }

        // GET: Pontuacoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Pontuacoes == null)
            {
                return NotFound();
            }

            var pontuacao = await _context.Pontuacoes.FindAsync(id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            return View(pontuacao);
        }

        // POST: Pontuacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Comentario,PontuacaoLimpeza,PontuacaoLocalizacao,PontuacaoQualidadePreco,PontuacaoEspaco,HabitacaoId")] Pontuacao pontuacao)
        {
            if (id != pontuacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    pontuacao.ApplicationUserId = userId;
                    pontuacao.DataCriacao = DateTime.UtcNow;

                    _context.Update(pontuacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontuacaoExists(pontuacao.Id))
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
            return View(pontuacao);
        }

        // GET: Pontuacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pontuacoes == null)
            {
                return NotFound();
            }

            var pontuacao = await _context.Pontuacoes
                .Include(p => p.ApplicationUser)
                .Include(p => p.Habitacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacao == null)
            {
                return NotFound();
            }

            return View(pontuacao);
        }

        // POST: Pontuacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pontuacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pontuacoes'  is null.");
            }
            var pontuacao = await _context.Pontuacoes.FindAsync(id);
            if (pontuacao != null)
            {
                _context.Pontuacoes.Remove(pontuacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontuacaoExists(int id)
        {
          return (_context.Pontuacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
