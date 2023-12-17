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
using Microsoft.AspNetCore.Identity;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LocadoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LocadoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Locadores
        public async Task<IActionResult> Index()
        {
            //TODO: Habitacoes e GestoresFuncionarios do locador
            var locadores = await _context.Locadores
                .Include(h => h.Habitacoes)
                .Include(h => h.GestoresFuncionarios)
                .ToListAsync();

            return View(locadores);
        }

        // GET: Locadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            //TODO: Habitacoes e GestoresFuncionarios do locador
            var locador = await _context.Locadores
                .Include(h => h.Habitacoes)
                .Include(h => h.GestoresFuncionarios)
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
            return View();
        }

        // POST: Locadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Contacto,IsEmpresa")] Locador locador)
        {
            ModelState.Remove(nameof(locador.Habitacoes));
            ModelState.Remove(nameof(locador.GestoresFuncionarios));

            if (ModelState.IsValid)
            {
                locador.DataCriacao = DateTime.UtcNow;
                locador.Ativo = true;
                _context.Add(locador);
                await _context.SaveChangesAsync();

                var defaultGestoruser = new ApplicationUser
                {
                    UserName = "gestor@" + locador.Nome.ToLower() + ".com",
                    Email = "gestor@" + locador.Nome.ToLower() + ".com",
                    PrimeiroNome = "Gestor",
                    UltimoNome = locador.Nome,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LocadorId = locador.Id,
                    Locador = locador,
                    CargoComLocador = "Gestor"
                };

                var user = await _userManager.FindByEmailAsync(locador.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(defaultGestoruser, "Is3C..00");
                    await _userManager.AddToRoleAsync(defaultGestoruser, "Gestor");
                    locador.GestoresFuncionarios.Add(defaultGestoruser);
                    await _context.SaveChangesAsync();
                }

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

            return View(locador);
        }

        // POST: Locadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Contacto,IsEmpresa")] Locador locador)
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
                .Include(h => h.Habitacoes)
                .Include(h => h.GestoresFuncionarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locador == null)
                return NotFound();

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
            if (locador == null)
                return NotFound();

            if (locador.Habitacoes != null)
                if (locador.Habitacoes.Count != 0)
                {
                    //TODO: popup de erro
                    return RedirectToAction(nameof(Index));
                }

            _context.Locadores.Remove(locador);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocadorExists(int id)
        {
          return (_context.Locadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Disable(int id)
        {
            if (_context.Locadores == null)
                return Problem("Entity set 'ApplicationDbContext.Locador'  is null.");

            var locador = await _context.Locadores.FindAsync(id);
            if (locador == null)
            {
                return NotFound();
            }

            locador.Ativo = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Enable(int id)
        {
            if (_context.Locadores == null)
                return Problem("Entity set 'ApplicationDbContext.Locador'  is null.");

            var locador = await _context.Locadores.FindAsync(id);
            if (locador == null)
            {
                return NotFound();
            }

            locador.Ativo = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
