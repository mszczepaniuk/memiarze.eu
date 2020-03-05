using memiarzeEu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> ListAdmins()
        {
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            return View(admins.ToList());
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            return View(new List<ApplicationUser>());
        }

        [HttpPost]
        public async Task<IActionResult> ListUsers(string search)
        {
            if (string.IsNullOrEmpty(search)) { return View(new List<ApplicationUser>()); }
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            var users = await userManager.GetUsersInRoleAsync("User");

            var result = users
                .AsQueryable()
                .Where(x => x.UserName.Contains(search))
                .Except(admins)
                .ToList();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null || await userManager.IsInRoleAsync(user, "Admin")) { return BadRequest(); }

            await userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("ListAdmins", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdminRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null || !await userManager.IsInRoleAsync(user, "Admin")) { return BadRequest(); }

            await userManager.RemoveFromRoleAsync(user, "Admin");

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}