using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using memiarzeEu.Models;
using memiarzeEu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using memiarzeEu.Data;
using Microsoft.EntityFrameworkCore;

namespace memiarzeEu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public HomeController(IHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Eager loading of ApplicationUser data
            var model = dbContext.Memes.Include(meme => meme.ApplicationUser).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMeme()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMeme(AddMemeViewModel model)
        {
            if (model.Image == null)
            {
                ModelState.AddModelError("", "Prosze wybrac zdjecie");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "img", "memes");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                Meme meme = new Meme
                {
                    Title = model.Title,
                    ImagePath = uniqueFileName,
                    CreationDate = DateTime.Now,
                    ApplicationUser = await userManager.GetUserAsync(User),
                };
                dbContext.Memes.Add(meme);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Cos poszlo nie tak podczas dodawania zdjecia");
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
