using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Afiliado> Afiliados { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Sindicato> Sindicatos { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Secre> Secres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehiculo>()
                .HasIndex(t => t.Nroplaca)
                .IsUnique();
        }
    }
}
