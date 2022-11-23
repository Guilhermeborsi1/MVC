using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using segundoMVC.Data;
using segundoMVC.EF;

namespace segundoMVC.Controllers
{
    public class GuilhermesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuilhermesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guilhermes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guilherme.ToListAsync());
        }

        // GET: Guilhermes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guilherme = await _context.Guilherme
                .FirstOrDefaultAsync(m => m.id == id);
            if (guilherme == null)
            {
                return NotFound();
            }

            return View(guilherme);
        }

        // GET: Guilhermes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guilhermes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nome,DataNascimento,ativo,sexo")] Guilherme guilherme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guilherme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guilherme);
        }

        // GET: Guilhermes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guilherme = await _context.Guilherme.FindAsync(id);
            if (guilherme == null)
            {
                return NotFound();
            }
            return View(guilherme);
        }

        // POST: Guilhermes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nome,DataNascimento,ativo,sexo")] Guilherme guilherme)
        {
            if (id != guilherme.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guilherme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuilhermeExists(guilherme.id))
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
            return View(guilherme);
        }

        // GET: Guilhermes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guilherme = await _context.Guilherme
                .FirstOrDefaultAsync(m => m.id == id);
            if (guilherme == null)
            {
                return NotFound();
            }

            return View(guilherme);
        }

        // POST: Guilhermes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guilherme = await _context.Guilherme.FindAsync(id);
            _context.Guilherme.Remove(guilherme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuilhermeExists(int id)
        {
            return _context.Guilherme.Any(e => e.id == id);
        }
    }
}
