using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;
using Transporte.Web.Helpers;

namespace Transporte.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Juan", "Zuluaga", "jzuluaga55@gmail.com", "350 634 2747", "Calle Luna Calle Sol", "Manager");
            var admin = await CheckUserAsync("2020", "Juan", "Zuluaga", "jzuluaga55@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", "Admin");
            var secre = await CheckUserAsync("3030", "Juan", "Zuluaga", "carlos.zuluaga@globant.com", "350 634 2747", "Calle Luna Calle Sol", "Secre");
            await CheckSindicatosAsync();
            await CheckManagerAsync(manager);
            await CheckAdminAsync(admin);
            await CheckSecreAsync(secre);
        }

        private async Task CheckSecreAsync(User user)
        {
            if (!_context.Secres.Any())
            {
                _context.Secres.Add(new Secre { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAdminAsync(User user)
        {
            if (!_context.Admins.Any())
            {
                _context.Admins.Add(new Admin { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Nombres = firstName,
                    Apellidos = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Direccion = address,
                    NroDocumento = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
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

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Secre");
        }
    }
}
