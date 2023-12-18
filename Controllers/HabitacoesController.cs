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
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using HabitAqui.Migrations;
using System.Diagnostics;

namespace HabitAqui.Controllers
{
    [Authorize(Roles = "Funcionario, Gestor")]
    public class HabitacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HabitacoesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Habitacoes
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var habitacoes = _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Alugueres)
                    .Where(h => h.LocadorId == currentUser.LocadorId);

                return View(await habitacoes.ToListAsync());
            }
            else
            {
                var habitacoes = _context.Habitacoes
                      .Include(h => h.Categoria)
                      .Include(h => h.Locador)
                      .Include(h => h.Tipologia);
                return View(await habitacoes.ToListAsync());
            }
        }

        // GET: Habitacoes/Details/5
        [AllowAnonymous]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitacoes == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes
                .Include(h => h.Categoria)
                .Include(h => h.Locador)
                .Include(h => h.Tipologia)
                .Include(h => h.Fotografias)
                .Include(h => h.Alugueres)
                    .ThenInclude(a => a.Pontuacao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (habitacao == null) {
                return NotFound();
            }

            DateTime dataCriacao = (DateTime) habitacao.Locador.DataCriacao;
            DateTime currentDate = DateTime.Now.Date;
            int anosDeServico = currentDate.Year - dataCriacao.Year;

            if (currentDate < dataCriacao.AddYears(anosDeServico)) {
                anosDeServico--;
            }

            ViewBag.AnosDeServico = anosDeServico;

            var habitacaoItens = await _context.Habitacoes_Itens
            .Where(hi => hi.HabitacaoId == habitacao.Id)
            .Include(hi => hi.Item)
            .ToListAsync();

            ViewBag.HabitacaoItens = habitacaoItens;

            double averagePontuacaoTotal = habitacao.Alugueres?.Where(a => a.Pontuacao?.MediaPontuacao.HasValue ?? false)
                                              .Select(a => a.Pontuacao.MediaPontuacao.Value)
                                              .DefaultIfEmpty(0)
                                              .Average() ?? 0;


            double averagePontuacaoLimpeza = habitacao.Alugueres?
                .Select(a => a.Pontuacao?.PontuacaoLimpeza)
                .Where(p => p.HasValue)
                .Average() ?? 0;

            double averagePontuacaoLocalizacao = habitacao.Alugueres?
                .Select(a => a.Pontuacao?.PontuacaoLocalizacao)
                .Where(p => p.HasValue)
                .Average() ?? 0;

            double averagePontuacaoQualidadePreco = habitacao.Alugueres?
                .Select(a => a.Pontuacao?.PontuacaoQualidadePreco)
                .Where(p => p.HasValue)
                .Average() ?? 0;

            double averagePontuacaoEspaco = habitacao.Alugueres?
                .Select(a => a.Pontuacao?.PontuacaoEspaco)
                .Where(p => p.HasValue)
                .Average() ?? 0;

            ViewBag.AveragePontuacaoTotal = averagePontuacaoTotal;
            ViewBag.AveragePontuacaoLimpeza = averagePontuacaoLimpeza;
            ViewBag.AveragePontuacaoLocalizacao = averagePontuacaoLocalizacao;
            ViewBag.AveragePontuacaoQualidadePreco = averagePontuacaoQualidadePreco;
            ViewBag.AveragePontuacaoEspaco = averagePontuacaoEspaco;

            return View(habitacao);
        }

        // GET: Habitacoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["TipologiaId"] = new SelectList(_context.Tipologia, "Id", "Nome");
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description");
            return View();
        }

        // POST: Habitacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descricao,CategoriaId,TipologiaId,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,Latitude,Longitude,Itens,Fotografias")] Habitacao habitacao, List<IFormFile> Fotografias)
        {
            //TODO: receber a(s) foto(s)
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                habitacao.DataCriacao = DateTime.UtcNow;
                habitacao.LocadorId = currentUser.LocadorId;
                _context.Add(habitacao);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(Request.Form["Itens"]))
                {
                    var selectedItens = Request.Form["Itens"].ToString().Split(',').Select(int.Parse).ToArray();

                    if (selectedItens.Length > 0)
                    {
                        foreach (var itemId in selectedItens)
                        {
                            var habitacaoItem = new Habitacao_Itens
                            {
                                HabitacaoId = habitacao.Id,
                                ItemId = itemId
                            };
                            _context.Add(habitacaoItem);
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "habitacoes");

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
                            HabitacaoId = habitacao.Id
                        };

                        _context.Add(fotografia);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
            ViewData["TipologiaId"] = new SelectList(_context.Tipologia, "Id", "Nome", habitacao.TipologiaId);
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Name", habitacao.Itens);

            return View(habitacao);
        }

        // GET: Habitacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitacoes == null) {
                return NotFound();
            }

            var habitacao = await _context.Habitacoes.FindAsync(id);
            if (habitacao == null) {
                return NotFound();
            }
            ViewData["LocadorNome"] = _context.Locadores.Find(habitacao.LocadorId)?.Nome;
            ViewData["TipologiaNome"] = _context.Tipologia.Find(habitacao.TipologiaId)?.Nome;
            ViewData["CategoriaNome"] = _context.Categorias.Find(habitacao.CategoriaId)?.Nome;

            var selectedDescriptions = _context.Habitacoes_Itens
                .Where(hi => hi.HabitacaoId == habitacao.Id)
                .Select(hi => hi.Item.Description)
                .ToList();

            ViewBag.Itens = new MultiSelectList(_context.Itens, "Description", "Description", selectedDescriptions);
            return View(habitacao);
        }

        // POST: Habitacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descricao,CategoriaId,TipologiaId,Pais,Distrito,Concelho,Rua,CustoPorNoite,NumPessoas,NumWC,Disponivel,LocadorId,Latitude,Longitude,Itens")] Habitacao habitacao)
        {
            if (id != habitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    habitacao.LocadorId = currentUser.LocadorId;
                    habitacao.DataCriacao = _context.Habitacoes.AsNoTracking().FirstOrDefault(h => h.Id == id)?.DataCriacao ?? DateTime.UtcNow;
                    _context.Update(habitacao);
                    await _context.SaveChangesAsync();

                    _context.Habitacoes_Itens.RemoveRange(_context.Habitacoes_Itens.Where(hi => hi.HabitacaoId == habitacao.Id));
                    await _context.SaveChangesAsync();

                    var selectedDescriptions = Request.Form["Itens"].ToString().Split(',');

                    foreach (var description in selectedDescriptions)
                    {
                        var itemId = _context.Itens
                            .Where(i => i.Description == description)
                            .Select(i => i.Id)
                            .FirstOrDefault();

                        if (itemId != null)
                        {
                            var habitacaoItem = new Habitacao_Itens
                            {
                                HabitacaoId = habitacao.Id,
                                ItemId = itemId
                            };
                            _context.Add(habitacaoItem);
                        }
                    }

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
            ViewData["LocadorNome"] = _context.Locadores.Find(habitacao.LocadorId)?.Nome;
            ViewData["TipologiaNome"] = _context.Tipologia.Find(habitacao.TipologiaId)?.Nome;
            ViewData["CategoriaNome"] = _context.Categorias.Find(habitacao.CategoriaId)?.Nome;
            ViewData["Itens"] = new MultiSelectList(_context.Itens, "Id", "Description", habitacao.Itens);
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
                .Include(h => h.Tipologia)
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
            var habitacao = await _context.Habitacoes
                .Include(h => h.Fotografias)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (habitacao != null)
            {
                _context.Habitacoes.Remove(habitacao);
            }

            foreach (var fotografia in habitacao.Fotografias)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/habitacoes", fotografia.Nome);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Habitacoes/Search
        [AllowAnonymous]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Search([Bind("TextoAPesquisar,CategoriaId,LocadorId,CheckIn,CheckOut,OrdemPreco,OrdemRating")] SearchViewModel search)
        {
            var categorias = await _context.Categorias
                .Select(c => new { Id = c.Id, Nome = c.Nome })
                .Distinct()
                .ToListAsync();
            categorias.Insert(0, new { Id = 0, Nome = "Todas" });
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            var locadores = await _context.Locadores
                .Select(l => new { Id = l.Id, Nome = l.Nome })
                .Distinct()
                .ToListAsync();
            locadores.Insert(0, new { Id = 0, Nome = "Todos" });
            ViewBag.Locadores = new SelectList(locadores, "Id", "Nome");

            ModelState.Remove(nameof(search.Habitacoes));

            if (search.CheckIn > search.CheckOut)
            {
                ModelState.AddModelError("CheckIn", "A data de check-in n�o pode ser superior � data de check-out.");
                ModelState.AddModelError("CheckOut", "A data de check-out n�o pode ser inferior � data de check-in.");
            }

            if (ModelState.IsValid)
            {

                search.Habitacoes = await _context.Habitacoes
                    .Include(h => h.Alugueres)
                        .ThenInclude(a => a.CheckIn)
                    .Include(h => h.Alugueres)
                        .ThenInclude(a => a.CheckOut)
                    .Include(h => h.Alugueres)
                        .ThenInclude(a => a.Pontuacao)
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Fotografias)
                    .Where(h => h.Disponivel == true)
                    .Where(h => h.Name.Contains(search.TextoAPesquisar) ||
                                h.Descricao.Contains(search.TextoAPesquisar))
                    .Where(h => h.Alugueres
                        .All(a => a.CheckIn.DataCheckIn > search.CheckOut ||
                                    a.CheckOut.DataCheckOut < search.CheckIn))
                    .ToListAsync();

                if (search.CategoriaId != 0)
                    search.Habitacoes = search.Habitacoes.Where(h => h.CategoriaId == search.CategoriaId).ToList();

                if (search.LocadorId != 0)
                    search.Habitacoes = search.Habitacoes.Where(h => h.LocadorId == search.LocadorId).ToList();

                if (search.OrdemPreco != null)
                {
                    if (search.OrdemPreco == "Ascendente")
                        search.Habitacoes = search.Habitacoes.OrderBy(h => h.CustoPorNoite).ToList();
                    else
                        search.Habitacoes = search.Habitacoes.OrderByDescending(h => h.CustoPorNoite).ToList();
                }

                if (search.OrdemRating != null)
                {
                    if (search.OrdemRating == "Ascendente")
                        search.Habitacoes = search.Habitacoes.OrderBy(h => h.Alugueres.Average(a => a.Pontuacao.MediaPontuacao)).ToList();
                    else
                        search.Habitacoes = search.Habitacoes.OrderByDescending(h => h.Alugueres.Average(a => a.Pontuacao.MediaPontuacao)).ToList();
                }

                //TODO
                //if (search.OrdemRating != null)
                //    if (search.OrdemRating == "Ascendente")
                //        search.Habitacoes = search.Habitacoes.OrderBy(h => h.Pontuacoes).ToList();
                //    else
                //        search.Habitacoes = search.Habitacoes.OrderByDescending(h => h.Pontuacoes).ToList();

                search.NumResultados = search.Habitacoes.Count();

                return View(search);
            } else
            {
                search.Habitacoes = await _context.Habitacoes
                    .Include(h => h.Categoria)
                    .Include(h => h.Locador)
                    .Include(h => h.Tipologia)
                    .Include(h => h.Fotografias)
                    .ToListAsync();

                //TODO: show model erro on view (texto a pesquisar, tipo, checkin, checkout)
            }

            return View("~/Views/Home/Index.cshtml", search);
        }

        private bool HabitacaoExists(int id)
        {
            return (_context.Habitacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Disable(int? id)
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

            habitacao.Disponivel = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Enable(int? id)
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

            habitacao.Disponivel = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
