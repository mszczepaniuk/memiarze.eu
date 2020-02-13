//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using memiarzeEu.Models;
//using memiarzeEu.ViewModels;
//using Microsoft.AspNetCore.Authorization;
//using System.IO;
//using Microsoft.Extensions.Hosting;
//using Microsoft.AspNetCore.Identity;
//using memiarzeEu.Data;
//using Microsoft.EntityFrameworkCore;

//namespace memiarzeEu.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IHostEnvironment hostEnvironment;
//        private readonly ApplicationDbContext dbContext;


//        public HomeController(IHostEnvironment hostEnvironment,
//            ApplicationDbContext dbContext)
//        {
//            this.hostEnvironment = hostEnvironment;
//            this.dbContext = dbContext;
//        }

//        public IActionResult Index(int page)
//        {
//            if (page == 0) { page = 1; }
//            IQueryable<Meme> model = dbContext.Memes.Include(meme => meme.ApplicationUser).Include(meme => meme.XdPoints).OrderByDescending(m => m.CreationDate);
//            var pages = model.Count() / memesPerPage + 1;
//            model = model.Skip(memesPerPage * (page - 1)).Take(memesPerPage);
//            if (page < 1 || page > pages) { return View("NotFound"); }
//            foreach (var meme in model)
//            {
//                if (dbContext.XdPoints
//                    .Where(a => a.ApplicationUser.UserName == User.Identity.Name)
//                    .Where(b => b.MemeId == meme.Id).Any())
//                {
//                    meme.IsXdClicked = true;
//                }
//            }
//            ViewBag.CurrentPage = page;
//            ViewBag.Pages = pages;
//            return View(model.ToList());
//        }

//        [HttpGet]
//        [Authorize]
//        public IActionResult AddMeme()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> AddMeme(AddMemeViewModel model)
//        {
//            if (model.Image == null)
//            {
//                ModelState.AddModelError("", "Prosze wybrac zdjecie");
//                return View(model);
//            }

//            if (ModelState.IsValid)
//            {
//                string uploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "img", "memes");
//                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
//                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    model.Image.CopyTo(fileStream);
//                }
//                Meme meme = new Meme
//                {
//                    Title = model.Title,
//                    ImagePath = uniqueFileName,
//                    CreationDate = DateTime.Now,
//                    ApplicationUser = await userManager.GetUserAsync(User),
//                };
//                dbContext.Memes.Add(meme);
//                dbContext.SaveChanges();
//                return RedirectToAction("index");
//            }
//            ModelState.AddModelError("", "Cos poszlo nie tak podczas dodawania zdjecia");
//            return View(model);
//        }

//        public async Task<IActionResult> RandomMeme()
//        {
//            var model = await dbContext.Memes.OrderBy(r => Guid.NewGuid()).Take(1).Include(meme => meme.ApplicationUser).Include(meme => meme.XdPoints).FirstAsync();
//            if (model == null) return NotFound();
//            if (dbContext.XdPoints.Where(a => a.ApplicationUser.UserName == User.Identity.Name)
//                                  .Where(b => b.MemeId == model.Id).Any())
//            {
//                model.IsXdClicked = true;
//            }
//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult DeleteMeme(int id)
//        {
//            var meme = dbContext.Memes.Find(id);
//            if (meme == null) return View("NotFound");
//            dbContext.Memes.Remove(meme);
//            dbContext.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
