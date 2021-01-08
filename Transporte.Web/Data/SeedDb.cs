using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckSindicatosAsync();
        }

        private async Task CheckSindicatosAsync()
        {
            if (!_context.Sindicatos.Any())
            {
                AddSindicato("La playa", "Pedro Apaza", "Mercado central", DateTime.Today, "63999871");
                AddSindicato("La catedral", "Juan Surita", "Plaza principal", DateTime.Today, "63549125");
                await _context.SaveChangesAsync();
            }
        }

        private void AddSindicato(string nomsind, string responsa, string ubicacion, DateTime fundacion, string celu)
        {
            _context.Sindicatos.Add(new Sindicato
            {
                Nomsindica = nomsind,
                Responsable = responsa,
                Ubicacion = ubicacion,
                Fechafundacion = fundacion,
                Celular = celu
            });
        }
    }
}
