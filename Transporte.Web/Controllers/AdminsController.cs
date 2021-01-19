using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data;
using Transporte.Web.Helpers;

namespace Transporte.Web.Controllers
{
    public class AdminsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AdminsController(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Admins.Include(m => m.User));
        }
    }
}
