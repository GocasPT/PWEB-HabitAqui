using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Data;
using HabitAqui.Models;

namespace HabitAqui.Controllers
{
    public class ModelTestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelTestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModelTests
        public async Task<IActionResult> Index()
        {
              return _context.ModelTest != null ? 
                          View(await _context.ModelTest.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ModelTest'  is null.");
        }

        // GET: ModelTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModelTest == null)
            {
                return NotFound();
            }

            var modelTest = await _context.ModelTest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelTest == null)
            {
                return NotFound();
            }

            return View(modelTest);
        }

        // GET: ModelTests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelTests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ModelTest modelTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelTest);
        }

        // GET: ModelTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModelTest == null)
            {
                return NotFound();
            }

            var modelTest = await _context.ModelTest.FindAsync(id);
            if (modelTest == null)
            {
                return NotFound();
            }
            return View(modelTest);
        }

        // POST: ModelTests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ModelTest modelTest)
        {
            if (id != modelTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelTestExists(modelTest.Id))
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
            return View(modelTest);
        }

        // GET: ModelTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ModelTest == null)
            {
                return NotFound();
            }

            var modelTest = await _context.ModelTest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelTest == null)
            {
                return NotFound();
            }

            return View(modelTest);
        }

        // POST: ModelTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModelTest == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ModelTest'  is null.");
            }
            var modelTest = await _context.ModelTest.FindAsync(id);
            if (modelTest != null)
            {
                _context.ModelTest.Remove(modelTest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelTestExists(int id)
        {
          return (_context.ModelTest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
