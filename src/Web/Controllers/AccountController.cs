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
using memiarzeEu.ViewModels.Account;
using memiarzeEu.Extensions;
using memiarzeEu.Interfaces;
using memiarzeEu.Specifications;
using memiarzeEu.ViewModels.Shared;
using memiarzeEu.Specifications.XdPointSpec;
using memiarzeEu.Specifications.MemeSpec;
using memiarzeEu.Specifications.CommentSpec;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAsyncRepository<Meme> memeRepo;
        private readonly IAsyncRepository<Comment> commentRepo;
        private readonly IAsyncRepository<MemeXdPoint> memeXdPointRepo;
        private readonly IAsyncRepository<CommentXdPoint> commentXdPointRepo;
        private readonly IAvatarFileService fileService;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAsyncRepository<Meme> memeRepo,
            IAsyncRepository<Comment> commentRepo,
            IAsyncRepository<MemeXdPoint> memeXdPointRepo,
            IAsyncRepository<CommentXdPoint> commentXdPointRepo,
            IAvatarFileService fileService,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.memeRepo = memeRepo;
            this.commentRepo = commentRepo;
            this.memeXdPointRepo = memeXdPointRepo;
            this.commentXdPointRepo = commentXdPointRepo;
            this.fileService = fileService;
            this.configuration = configuration;
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

                ModelState.AddModelError("", "Błędna próba logowania.");
            }
            return View(model);
        }

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
                var user = new ApplicationUser { UserName = model.UserName, AvatarPath = configuration.GetSection("DefaultAvatarPath").Value };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Doszło do błędu podczas próby rejestracji.");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUsernameTaken(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Nazwa {username} jest juz zajęta.");
            }
        }

        public async Task<IActionResult> TopMemes(string userId, int memePage)
        {
            int maxNumberOfPages = (await memeRepo.CountAsync(new CountUserMemesSpec(userId)) - 1) / 10 + 1;
            if (maxNumberOfPages == 0) { maxNumberOfPages = 1; }

            if (memePage < 1 || memePage > maxNumberOfPages) { return NotFound(); }

            var currentUserId = this.GetCurrentUserId();

            var memes = await memeRepo.GetAsync(new PageOfMemesUserTopSpec(userId, memePage));
            var memeViewModels = new List<MemeCardViewModel>();

            foreach (var meme in memes)
            {
                var userPoint = await memeXdPointRepo.GetAsync(new MemeXdPointConcreteUserIdAndMemeIdSpec(currentUserId, meme.Id));
                var isXdClicked = userPoint.FirstOrDefault() != null ? true : false;
                memeViewModels.Add(new MemeCardViewModel(meme, isXdClicked, configuration));
            }

            var paginationViewModel = new PaginationViewModel()
            {
                ActionName = "TopMemes",
                ControllerName = "Account",
                CurrentPage = memePage,
                MaxNumberOfPages = maxNumberOfPages,
                AllRouteData = new Dictionary<string, string> { { "userId", userId } },
                AlternativePageName = "memePage"
            };

            return View(new TopMemesViewModel
            {
                User = await userManager.FindByIdAsync(userId),
                MemeCardViewModels = memeViewModels,
                PaginationViewModel = paginationViewModel
            });
        }

        public async Task<IActionResult> TopComments(string userId, int commentPage)
        {
            int maxNumberOfPages = (await commentRepo.CountAsync(new CountUserCommentsSpec(userId)) - 1) / 10 + 1;
            if (maxNumberOfPages == 0) { maxNumberOfPages = 1; }

            if (commentPage < 1 || commentPage > maxNumberOfPages) { return NotFound(); }

            var currentUserId = this.GetCurrentUserId();

            var comments = await commentRepo.GetAsync(new PageOfCommentsUserTopSpec(userId, commentPage));
            var commentViewModels = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                var userPoint = await commentXdPointRepo.GetAsync(new CommentXdPointConcreteUserIdAndMemeIdSpec(currentUserId, comment.Id));
                var isXdClicked = userPoint.FirstOrDefault() != null ? true : false;
                commentViewModels.Add(new CommentViewModel(comment, isXdClicked, configuration));
            }

            var paginationViewModel = new PaginationViewModel()
            {
                ActionName = "TopComments",
                ControllerName = "Account",
                CurrentPage = commentPage,
                MaxNumberOfPages = maxNumberOfPages,
                AllRouteData = new Dictionary<string, string> { { "userId", userId } },
                AlternativePageName = "commentPage"
            };

            return View(new TopCommentsViewModel
            {
                User = await userManager.FindByIdAsync(userId),
                CommentViewModels = commentViewModels,
                PaginationViewModel = paginationViewModel
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser(string id)
        {
            if (this.GetCurrentUserId() != id)
            {
                return Forbid();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
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
            if (this.GetCurrentUserId() != model.Id)
            {
                return Forbid();
            }
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);

                if (model.Avatar != null)
                {
                    if (user.AvatarPath != null) { fileService.Delete(user.AvatarPath); }
                    user.AvatarPath = fileService.Save(model.Avatar);
                }

                user.About = model.About;
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cos poszlo nie tak podczas aktualizowania profilu.");
                    return View(model);
                }

                return RedirectToAction("TopMemes", "Account", new { userId = model.Id, memePage = 1 });
            }
            ModelState.AddModelError("", "Cos poszlo nie tak.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!(User.IsInRole("Admin") || this.GetCurrentUserId() == id))
            {
                return Forbid();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await userManager.DeleteAsync(user);
            if (user.AvatarPath != null) { fileService.Delete(user.AvatarPath); }
            return RedirectToAction("Index", "Home");
        }
    }
}
