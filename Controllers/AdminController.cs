using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace memiarzeEu.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await userManager.GetUsersInRoleAsync("Admin");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(string userName)
        {
            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(userName), "Admin");
            return RedirectToAction("Index");
        }
    }
}