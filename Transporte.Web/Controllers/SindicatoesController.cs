using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transporte.Web.Data;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Controllers
{
    public class SindicatoesController : Controller
    {
        private readonly DataContext _context;

        public SindicatoesController(DataContext context)
        {
            _context = context;
        }

        // GET: Sindicatoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sindicatos.ToListAsync());
        }

        // GET: Sindicatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return View(sindicato);
        }

        // GET: Sindicatoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sindicatoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nomsindica,Responsable,Ubicacion,Fechafundacion,Celular")] Sindicato sindicato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sindicato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sindicato);
        }

        // GET: Sindicatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos.FindAsync(id);
            if (sindicato == null)
            {
                return NotFound();
            }
            return View(sindicato);
        }

        // POST: Sindicatoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nomsindica,Responsable,Ubicacion,Fechafundacion,Celular")] Sindicato sindicato)
        {
            if (id != sindicato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sindicato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SindicatoExists(sindicato.Id))
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
            return View(sindicato);
        }

        // GET: Sindicatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return View(sindicato);
        }

        // POST: Sindicatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sindicato = await _context.Sindicatos.FindAsync(id);
            _context.Sindicatos.Remove(sindicato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SindicatoExists(int id)
        {
            return _context.Sindicatos.Any(e => e.Id == id);
        }
    }
}
