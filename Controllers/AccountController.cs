using memiarzeEu.Data;
using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace memiarzeEu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHostEnvironment hostEnvironment;
        private readonly ApplicationDbContext dbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostEnvironment hostEnvironment, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
                ModelState.AddModelError("", "Błedna próba logowania");
            }
            return View(model);
        }
        // TODO: Fix views not loading after registering, loging or editing user.
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return View("NotFound");
            user.Memes = dbContext.Memes.Where(m => m.ApplicationUserId == id).Include(m => m.XdPoints).OrderByDescending(m => m.CreationDate).ToList();
            return View(user);
        }

        // TODO: Check authorization.
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return View("NotFound");
            // Hacky authorization
            if (user.UserName != User.Identity.Name) throw new InvalidCredentialException();
            var model = new EditUserViewModel
            {
                Id = id,
                About = user.About
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                user.AvatarPath = model.CurrentAvatarPath;
                if (model.Avatar != null)
                {
                    string uploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "img", "avatars");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fileStream);
                    }

                    if ((user.AvatarPath != null) && (System.IO.File.Exists(Path.Combine(uploadsFolder, user.AvatarPath))))
                    {
                        string oldAvatarPath = Path.Combine(uploadsFolder, user.AvatarPath);
                        System.IO.File.Delete(oldAvatarPath);
                    }

                    user.AvatarPath = uniqueFileName;
                }

                user.About = model.About;
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cos poszlo nie tak podczas aktualizowania zdjecia");
                    return View(model);
                }

                return RedirectToAction("Index", "Account", new { id = model.Id });
            }
            ModelState.AddModelError("", "Cos poszlo nie tak");
            return View(model);
        }

        //TODO: Add succes confirmation page. Add authorization for users wanting to delete their own accounts.
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return View("NotFound");
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> TopCarousel(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return View("NotFound");
            user.Memes = dbContext.Memes.Where(m => m.ApplicationUserId == id).Include(m => m.XdPoints).OrderByDescending(m => m.XdPoints.Count).Take(3).ToList();
            return View(user);
        }

        public async Task<IActionResult> TopLightBox(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return View("NotFound");
            user.Memes = dbContext.Memes.Where(m => m.ApplicationUserId == id).Include(m => m.XdPoints).OrderByDescending(m => m.XdPoints.Count).Take(3).ToList();
            return View(user);
        }
    }
}
