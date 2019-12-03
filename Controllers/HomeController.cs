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
            var model = dbContext.Memes.Include(meme => meme.ApplicationUser).Include(meme => meme.XdPoints).OrderByDescending(m => m.CreationDate).ToList();
            foreach (var meme in model)
            {
                if (dbContext.XdPoints
                    .Where(a => a.ApplicationUser.UserName == User.Identity.Name)
                    .Where(b => b.MemeId == meme.Id).Any())
                {
                    meme.IsXdClicked = true;
                }

            }
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

        public async Task<IActionResult> RandomMeme()
        {
            // TODO Understand randomizer.
            var model = await dbContext.Memes.OrderBy(r => Guid.NewGuid()).Take(1).Include(meme => meme.ApplicationUser).Include(meme => meme.XdPoints).FirstAsync();
            if (model == null) return NotFound();
            if (dbContext.XdPoints.Where(a => a.ApplicationUser.UserName == User.Identity.Name)
                                  .Where(b => b.MemeId == model.Id).Any())
            {
                model.IsXdClicked = true;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AwardXdPoint(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var query = dbContext.XdPoints.Any(x => ((x.ApplicationUserId == user.Id) && (x.MemeId == id)));
            if (!query)
            {
                var xDpoint = new XdPoint()
                {
                    ApplicationUserId = user.Id,
                    MemeId = id,
                    
                };
                dbContext.XdPoints.Add(xDpoint);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveXdPoint(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var query = dbContext.XdPoints.Where(m => m.MemeId == id).Where(u => u.ApplicationUserId == user.Id);
            if (query.Any())
            {
                var xDpoint = query.First();
                dbContext.Remove(xDpoint);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteMeme(int id)
        {
            var meme = dbContext.Memes.Find(id);
            if (meme == null) return View("NotFound");
            dbContext.Memes.Remove(meme);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
