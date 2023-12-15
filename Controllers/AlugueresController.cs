using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Cliente, Funcionario, Gestor")]
    public class AlugueresController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlugueresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Alugueres
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Cliente"))
            {
                var clientAlugueres = await _context.Alugueres
                    .Where(a => a.ClienteId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Include(a => a.Habitacao)
                    .Include(a => a.Habitacao.Pontuacoes)
                    .Include(a => a.Locador)
                    .Include(a => a.CheckIn)
                    .Include(a => a.CheckOut)
                    .ToListAsync();

                return View(clientAlugueres);
            }

            var applicationDbContext = _context.Alugueres
                .Include(a => a.Habitacao)
                .Include(a => a.Locador)
                .Include(a => a.CheckIn)
                .Include(a => a.CheckOut)
                .Include(a => a.Cliente);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alugueres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alugueres == null)
            {
                return NotFound();
            }

            var aluguer = await _context.Alugueres
                .Include(a => a.Habitacao)
                .Include(a => a.Locador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguer == null)
            {
                return NotFound();
            }

            return View(aluguer);
        }

        // GET: Alugueres/Create
        public IActionResult Create(int HabitacaoId)
        {
            ViewBag.HabitacaoId = HabitacaoId;
            return View();
        }

        // POST: Alugueres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataDeEntrada,DataDeSaida,HabitacaoId,LocadorId,ClienteId,Confirmado")] Aluguer aluguer)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                aluguer.ClienteId = currentUser.Id;
                aluguer.Confirmado = false;
                var locadorId = _context.Habitacoes
                    .Where(h => h.Id == aluguer.HabitacaoId)
                    .Select(h => h.LocadorId)
                    .FirstOrDefault();
                aluguer.LocadorId = locadorId;
                _context.Add(aluguer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluguer);
        }

        // GET: Alugueres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alugueres == null)
            {
                return NotFound();
            }

            var aluguer = await _context.Alugueres.FindAsync(id);
            if (aluguer == null)
            {
                return NotFound();
            }
            ViewData["HabitacaoId"] = new SelectList(_context.Habitacoes, "Id", "Id", aluguer.HabitacaoId);
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Id", aluguer.LocadorId);
            return View(aluguer);
        }

        // POST: Alugueres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataDeEntrada,DataDeSaida,Confirmado,HabitacaoId,LocadorId")] Aluguer aluguer)
        {
            if (id != aluguer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluguer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AluguerExists(aluguer.Id))
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
            ViewData["HabitacaoId"] = new SelectList(_context.Habitacoes, "Id", "Id", aluguer.HabitacaoId);
            ViewData["LocadorId"] = new SelectList(_context.Locadores, "Id", "Id", aluguer.LocadorId);
            return View(aluguer);
        }

        // GET: Alugueres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alugueres == null)
            {
                return NotFound();
            }

            var aluguer = await _context.Alugueres
                .Include(a => a.Habitacao)
                .Include(a => a.Locador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguer == null)
            {
                return NotFound();
            }

            return View(aluguer);
        }

        // POST: Alugueres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alugueres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aluguer'  is null.");
            }
            var aluguer = await _context.Alugueres.FindAsync(id);
            if (aluguer != null)
            {
                _context.Alugueres.Remove(aluguer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AluguerExists(int id)
        {
            return (_context.Alugueres?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Disable(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var aluguer = await _context.Alugueres.FindAsync(id);
            if (aluguer == null)
            {
                return NotFound();
            }

            aluguer.Confirmado = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var aluguer = await _context.Alugueres.FindAsync(id);
            if (aluguer == null)
            {
                return NotFound();
            }

            aluguer.Confirmado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
