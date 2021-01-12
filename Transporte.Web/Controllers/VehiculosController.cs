using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly DataContext _context;

        public VehiculosController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscarVehiculo(string texto = "")
        {
            Vehiculo vehiculo = null;

            if (texto != "")
            {
                vehiculo = await _context.Vehiculos
                    .Include(v => v.Afiliado)
                    .FirstOrDefaultAsync(v => v.Nroplaca.Equals(texto.ToUpper()));
                if (vehiculo == null)
                {
                    return RedirectToAction(nameof(BuscarVehiculo));
                }
            }

            return View(vehiculo);
        }

        public async Task<IActionResult> BuscarSindicato(string texto = "")
        {
           
            var si = from m in _context.Sindicatos select m;

            if (texto !="")
            {
                si = si.Where(s => s.Nomsindica.Contains(texto));
            }
            else
            {
                si = null;
                return View(si);
            }
            return View(await si.ToListAsync());
        }

        public IActionResult DetailsAfili(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction($"Details/{id}", "Sindicatoes");
        }
    }
}
