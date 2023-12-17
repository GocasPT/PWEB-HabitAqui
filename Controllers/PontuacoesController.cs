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
using Microsoft.AspNetCore.Authorization;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Cliente")]
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
                    .Include(p => p.Aluguer)
                        .ThenInclude(a => a.Habitacao)
                    .Where(p => p.ApplicationUserId == currentUserId)
                    .ToListAsync();

                return View(pontuacoes);
            }

            var applicationDbContext = _context.Pontuacoes.Include(p => p.ApplicationUser).Include(p => p.Aluguer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pontuacoes/Create
        public IActionResult Create(int? AluguerId)
        {
            if (AluguerId == null || _context.Pontuacoes == null)
            {
                return NotFound();

            }
            ViewBag.AluguerId = AluguerId;
            return View();
        }

        // POST: Pontuacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Comentario,PontuacaoLimpeza,PontuacaoLocalizacao,PontuacaoQualidadePreco,PontuacaoEspaco,AluguerId")] Pontuacao pontuacao)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                pontuacao.ApplicationUserId = userId;

                double average = (pontuacao.PontuacaoLimpeza + pontuacao.PontuacaoLocalizacao + pontuacao.PontuacaoQualidadePreco + pontuacao.PontuacaoEspaco) / 4.0;

                pontuacao.MediaPontuacao = (int)average;
                pontuacao.DataCriacao = DateTime.UtcNow;

                var aluguer = await _context.Alugueres.FindAsync(pontuacao.AluguerId);

                aluguer.Pontuacao = pontuacao;
                aluguer.PontuacaoId = pontuacao.Id;

                _context.Add(pontuacao);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Alugueres");
            }

            return View(pontuacao);
        }

        // GET: Pontuacoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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
                return Problem("Entity set 'ApplicationDbContext.Pontuacoes' is null.");
            }

            var pontuacao = await _context.Pontuacoes
                .Include(p => p.Aluguer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pontuacao == null)
            {
                return NotFound();
            }

            if (pontuacao.Aluguer != null)
            {
                pontuacao.Aluguer.Pontuacao = null;
                pontuacao.Aluguer.PontuacaoId = null;
            }

            _context.Pontuacoes.Remove(pontuacao);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
