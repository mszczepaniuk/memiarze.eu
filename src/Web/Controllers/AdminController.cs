using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using memiarzeEu.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace memiarzeEu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ListAdmins()
        {
            var model = new List<ApplicationUser>();
            var adminRoleId = dbContext.Roles.Single(a => a.NormalizedName == "ADMIN").Id;
            var userRoles = dbContext.UserRoles.Where(a => a.RoleId == adminRoleId).ToList();
            foreach (var userRole in userRoles)
            {
                model.Add(await dbContext.Users.FindAsync(userRole.UserId));
            }
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> ListUsers()
        //{
        //    var model = new List<ApplicationUser>();
        //    var adminRoleId = dbContext.Roles.Single(a => a.NormalizedName == "ADMIN").Id;
        //    foreach (var user in dbContext.Users.ToList())
        //    {
        //        if (!dbContext.UserRoles.Where(a => a.UserId == user.Id).Any(a => a.RoleId == adminRoleId))
        //        {
        //            model.Add(user);
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> AddAdminRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null) { await userManager.AddToRoleAsync(user, "Admin"); }
            else { return NotFound(); }
            return RedirectToAction("ListAdmins", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdminRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null) { await userManager.RemoveFromRoleAsync(user, "Admin"); }
            else { return NotFound(); }
            return RedirectToAction("ListUsers", "Admin");
        }
    }
}