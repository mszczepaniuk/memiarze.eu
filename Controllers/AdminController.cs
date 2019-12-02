using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace memiarzeEu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        //TODO Fix validation
        public async Task<IActionResult> Index(AdminIndexViewModel model)
        {
            if (model.Admins == null)
            {
                model.Admins = await userManager.GetUsersInRoleAsync("Admin");
                ModelState.Clear();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null) { await userManager.AddToRoleAsync(user, "Admin"); }
                else { return View("NotFound"); }
            }
            return RedirectToAction("Index", model);
        }
    }
}