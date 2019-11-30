using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(LoginViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(RegisterViewModel model)
        {
            return View(model);
        }
    }
}
