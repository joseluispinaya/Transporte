using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Afiliado> Afiliados { get; set; }

        public DbSet<Sindicato> Sindicatos { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }
    }
}
