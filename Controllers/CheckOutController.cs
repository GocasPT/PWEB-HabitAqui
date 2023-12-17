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
using HabitAqui.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Funcionario, Gestor")]
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CheckOutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: CheckOut
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CheckOuts.Include(c => c.Aluguer).Include(c => c.Funcionario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CheckOut/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOut = await _context.CheckOuts
                .Include(c => c.Aluguer)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkOut == null)
            {
                return NotFound();
            }

            return View(checkOut);
        }

        // GET: CheckOut/Create
        public IActionResult Create(int AluguerId)
        {
            ViewBag.AluguerId = AluguerId;
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View();
        }

        // POST: CheckOut/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Observacoes,Danos,CheckOutItems,FuncionarioId,AluguerId,Fotografias")] CheckOut checkOut, List<IFormFile> Fotografias)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                checkOut.FuncionarioId = currentUser.Id;
                checkOut.DataCheckOut = DateTime.UtcNow;

                _context.Add(checkOut);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(Request.Form["CheckOutItems"]))
                {
                    var selectedItens = Request.Form["CheckOutItems"].ToString().Split(',').Select(int.Parse).ToArray();

                    if (selectedItens.Length > 0)
                    {
                        foreach (var itemId in selectedItens)
                        {
                            var checkOutItem = new CheckOutItem
                            {
                                CheckOutId = checkOut.Id,
                                ItemId = itemId
                            };
                            _context.Add(checkOutItem);
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "checkOuts");

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
                            CheckOutId = checkOut.Id
                        };

                        _context.Add(fotografia);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index", "Alugueres");
            }

            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View(checkOut);
        }

        // POST: CheckOut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Observacoes,FuncionarioId,AluguerId")] CheckOut checkOut)
        {
            if (id != checkOut.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckOutExists(checkOut.Id))
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
            ViewData["AluguerId"] = new SelectList(_context.Alugueres, "Id", "Id", checkOut.AluguerId);
            ViewData["FuncionarioId"] = new SelectList(_context.Administradores, "Id", "Id", checkOut.FuncionarioId);
            return View(checkOut);
        }

        // GET: CheckOut/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOut = await _context.CheckOuts
                .Include(c => c.Aluguer)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkOut == null)
            {
                return NotFound();
            }

            return View(checkOut);
        }

        // POST: CheckOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckOuts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CheckOuts'  is null.");
            }
            var checkOut = await _context.CheckOuts.FindAsync(id);
            if (checkOut != null)
            {
                _context.CheckOuts.Remove(checkOut);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckOutExists(int id)
        {
          return (_context.CheckOuts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
