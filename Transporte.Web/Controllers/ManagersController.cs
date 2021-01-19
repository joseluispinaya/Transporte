using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data;
using Transporte.Web.Data.Entities;
using Transporte.Web.Helpers;
using Transporte.Web.Models;

namespace Transporte.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ManagersController(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Managers.Include(m => m.User));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Direccion = model.Address,
                    NroDocumento = model.Document,
                    Email = model.Username,
                    Nombres = model.FirstName,
                    Apellidos = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username
                };
                var response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Manager");

                    var manager = new Manager { User = userInDB };

                    _context.Managers.Add(manager);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                
            }

            return View(model);
        }
    }
}
