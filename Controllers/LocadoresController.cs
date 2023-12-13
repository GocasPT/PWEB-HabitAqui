using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LocadoresController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public LocadoresController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // GET: Locadores
        public async Task<IActionResult> Index()
        {
            var locadores = _context.Locadores
                .Include(h => h.TipoLocador);

            return View(await locadores.ToListAsync());
        }

        // GET: Locadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores
                .Include(h => h.TipoLocador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locador == null)
            {
                return NotFound();
            }

            return View(locador);
        }

        // GET: Locadores/Create
        public IActionResult Create()
        {
            ViewData["TipoLocadorId"] = new SelectList(_context.TipoLocador, "Id", "Nome");
            return View();
        }

        // POST: Locadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Contacto,TipoLocadorId")] Locador locador)
        {
            ModelState.Remove(nameof(locador.Habitacoes));

            if (ModelState.IsValid)
            {
                locador.DataCriacao = DateTime.UtcNow;
                _context.Add(locador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locador);
        }

        // GET: Locadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores.FindAsync(id);
            if (locador == null)
            {
                return NotFound();
            }

            ViewData["TipoLocadorId"] = new SelectList(_context.TipoLocador, "Id", "Nome", locador.TipoLocador);

            return View(locador);
        }

        // POST: Locadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Contacto,Tipo,TipoLocadorId")] Locador locador)
        {
            if (id != locador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    locador.DataCriacao = _context.Locadores.AsNoTracking().FirstOrDefault(h => h.Id == id)?.DataCriacao ?? DateTime.UtcNow;
                    _context.Update(locador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocadorExists(locador.Id))
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
            return View(locador);
        }

        // GET: Locadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores
                .Include(h => h.TipoLocador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locador == null)
            {
                return NotFound();
            }

            return View(locador);
        }

        // POST: Locadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locadores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locador'  is null.");
            }
            var locador = await _context.Locadores.FindAsync(id);
            if (locador != null)
            {
                _context.Locadores.Remove(locador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocadorExists(int id)
        {
          return (_context.Locadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
