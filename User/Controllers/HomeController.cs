using Clients.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using User.Data;
using User.Models;
using User.Repository;
using User.Utils.General;

namespace User.Controllers
{
    public class HomeController : BaseController
    {
        //protected readonly IDDLRepository repository;
        protected readonly IFunctionRepository function;

        public HomeController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IFunctionRepository _function) : base(context, signInManager, userManager)
        {
            //repository = _repository;
            function = _function;
        }

        [Description("Endrit Balaj", "Index Method", " ")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            UserModel model = new();
            var users = await _context.Users.ToListAsync();
            model.Details = users;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
