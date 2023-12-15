using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace HabitAqui.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckInController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CheckIn
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CheckIns.Include(c => c.Aluguer).Include(c => c.Funcionario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CheckIn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns
                .Include(c => c.Aluguer)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkIn == null)
            {
                return NotFound();
            }

            return View(checkIn);
        }

        // GET: CheckIn/Create
        public IActionResult Create(int AluguerId)
        {
            ViewBag.AluguerId = AluguerId;
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View();
        }

        // POST: CheckIn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Observacoes,Danos,CheckInItems,FuncionarioId,AluguerId")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                checkIn.FuncionarioId = currentUser.Id;
                checkIn.DataCheckIn = DateTime.UtcNow;

                _context.Add(checkIn);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(Request.Form["CheckInItems"]))
                {
                    var selectedItens = Request.Form["CheckInItems"].ToString().Split(',').Select(int.Parse).ToArray();

                    if (selectedItens.Length > 0)
                    {
                        foreach (var itemId in selectedItens)
                        {
                            var checkInItem = new CheckInItem
                            {
                                CheckInId = checkIn.Id,
                                ItemId = itemId
                            };
                            _context.Add(checkInItem);
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index", "Alugueres");
            }

            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View(checkIn);
        }

        // GET: CheckIn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns.FindAsync(id);
            if (checkIn == null)
            {
                return NotFound();
            }
            ViewData["AluguerId"] = new SelectList(_context.Alugueres, "Id", "Id", checkIn.AluguerId);
            ViewData["FuncionarioId"] = new SelectList(_context.Administradores, "Id", "Id", checkIn.FuncionarioId);
            return View(checkIn);
        }

        // POST: CheckIn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Observacoes,FuncionarioId,AluguerId")] CheckIn checkIn)
        {
            if (id != checkIn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckInExists(checkIn.Id))
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
            ViewData["AluguerId"] = new SelectList(_context.Alugueres, "Id", "Id", checkIn.AluguerId);
            ViewData["FuncionarioId"] = new SelectList(_context.Administradores, "Id", "Id", checkIn.FuncionarioId);
            return View(checkIn);
        }

        // GET: CheckIn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns
                .Include(c => c.Aluguer)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkIn == null)
            {
                return NotFound();
            }

            return View(checkIn);
        }

        // POST: CheckIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckIns == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CheckIns'  is null.");
            }
            var checkIn = await _context.CheckIns.FindAsync(id);
            if (checkIn != null)
            {
                _context.CheckIns.Remove(checkIn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckInExists(int id)
        {
          return (_context.CheckIns?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
