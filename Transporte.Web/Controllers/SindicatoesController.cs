using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transporte.Web.Data;
using Transporte.Web.Data.Entities;
using Transporte.Web.Models;

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
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Sindicatos.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View(_context.Sindicatos
                .Include(o => o.Afiliados));
        }
        // GET: Sindicatoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sindicato = await _context.Sindicatos
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sindicato == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sindicato);
        //}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sindicato = await _context.Sindicatos
                .Include(a=> a.Afiliados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sindicato == null)
            {
                return NotFound();
            }

            return View(sindicato);
        }

        public async Task<IActionResult> AddAfiliado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sindica = await _context.Sindicatos.FindAsync(id);
            if (sindica == null)
            {
                return NotFound();
            }
            var model = new AfiliadoViewModel
            {
                SindicatoId = sindica.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAfiliado(AfiliadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var afili = await ToAfiliadoAsync(model, true);
                _context.Afiliados.Add(afili);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.SindicatoId}");
            }
            return View(model);
        }

        public async Task<Afiliado> ToAfiliadoAsync(AfiliadoViewModel model, bool isNew)
        {
            return new Afiliado
            {
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Direccion = model.Direccion,
                Id = isNew ? 0 : model.Id,
                NroDocumento = model.NroDocumento,
                Celular = model.Celular,
                Foto = await UploadImageAsync(model.ImageFile),
                Imgqr = "nada",
                Sindicato = await _context.Sindicatos.FindAsync(model.SindicatoId)

            };
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images\\Afili",
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/Afili/{file}";
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
