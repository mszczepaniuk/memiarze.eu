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
using memiarzeEu.Interfaces;
using memiarzeEu.Specifications.MemeSpec;
using System.Security.Claims;
using memiarzeEu.Specifications.XdPointSpec;
using memiarzeEu.Specifications;
using memiarzeEu.Extensions;
using memiarzeEu.Specifications.CommentSpec;
using memiarzeEu.ViewModels.Home;
using memiarzeEu.ViewModels.Shared;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAsyncRepository<Meme> memeRepo;
        private readonly IAsyncRepository<MemeXdPoint> memeXdPointRepo;
        private readonly IAsyncRepository<CommentXdPoint> commentXdPointRepo;
        private readonly IFileService memeFileService;
        private readonly IAsyncRepository<Comment> commentRepo;
        private readonly IConfiguration configuration;

        public HomeController(IAsyncRepository<Meme> memeRepo,
            IAsyncRepository<MemeXdPoint> memeXdPointRepo,
            IAsyncRepository<CommentXdPoint> commentXdPointRepo,
            IMemeFileService memeFileService,
            IAsyncRepository<Comment> commentRepo,
            IConfiguration configuration)
        {
            this.memeRepo = memeRepo;
            this.memeXdPointRepo = memeXdPointRepo;
            this.commentXdPointRepo = commentXdPointRepo;
            this.memeFileService = memeFileService;
            this.commentRepo = commentRepo;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            page ??= new int?(1);
            int maxNumberOfPages = (await memeRepo.CountAsync(new BaseSpecification<Meme>()) - 1) / 10 + 1;
            if (maxNumberOfPages == 0) { maxNumberOfPages = 1; }

            if (page.Value < 1 || page.Value > maxNumberOfPages) { return NotFound(); }

            var userId = this.GetCurrentUserId();

            var memes = await memeRepo.GetAsync(new PageOfMemesOrderedByDateSpec(page.Value));
            var memeViewModels = new List<MemeCardViewModel>();

            foreach (var meme in memes)
            {
                var userPoint = await memeXdPointRepo.GetAsync(new MemeXdPointConcreteUserIdAndMemeIdSpec(userId, meme.Id));
                var isXdClicked = (userId == null || userPoint.FirstOrDefault() == null) ? false : true;
                memeViewModels.Add(new MemeCardViewModel(meme, isXdClicked, configuration));
            }

            var paginationViewModel = new PaginationViewModel()
            {
                ActionName = "Index",
                ControllerName = "Home",
                CurrentPage = page.Value,
                MaxNumberOfPages = maxNumberOfPages
            };

            return View(new IndexViewModel { MemeCardViewModels = memeViewModels, PaginationViewModel = paginationViewModel });
        }

        [HttpGet]
        public async Task<IActionResult> SingleMeme(int id, int commentPage)
        {
            int maxNumberOfPages = (await commentRepo.CountAsync(new CountCommentsOnMeme(id)) - 1) / 20 + 1;
            if (maxNumberOfPages == 0) { maxNumberOfPages = 1; }

            if (commentPage < 0 || commentPage > maxNumberOfPages) { return NotFound(); }

            var memes = await memeRepo.GetAsync(new SingleMemeSpec(id));
            var meme = memes.FirstOrDefault();

            if (meme == null) { return NotFound(); }

            var userId = this.GetCurrentUserId();

            var memeUserPoint = await memeXdPointRepo.GetAsync(new MemeXdPointConcreteUserIdAndMemeIdSpec(userId, meme.Id));
            var isMemeXdClicked = memeUserPoint.FirstOrDefault() != null ? true : false;

            var comments = await commentRepo.GetAsync(new PageOfCommentsSingleMemeSpec(commentPage, id));
            var commentViewModels = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                var userPoint = await commentXdPointRepo.GetAsync(new CommentXdPointConcreteUserIdAndMemeIdSpec(userId, comment.Id));
                var isXdClicked = userPoint.FirstOrDefault() != null ? true : false;
                commentViewModels.Add(new CommentViewModel(comment, isXdClicked, configuration));
            }

            var paginationViewModel = new PaginationViewModel()
            {
                ActionName = "SingleMeme",
                ControllerName = "Home",
                CurrentPage = commentPage,
                MaxNumberOfPages = maxNumberOfPages,
                AllRouteData = new Dictionary<string, string> { { "id", meme.Id.ToString() } },
                AlternativePageName = "commentPage"
            };

            return View(new SingleMemeViewModel
            {
                MemeCardViewModel = new MemeCardViewModel(meme, isMemeXdClicked, configuration),
                CommentViewModels = commentViewModels,
                PaginationViewModel = paginationViewModel
            });
        }

        [HttpGet]
        public async Task<IActionResult> RandomMeme()
        {
            var memeCount = await memeRepo.CountAsync(new BaseSpecification<Meme>());
            var memes = await memeRepo.GetAsync(new RandomElementSpec<Meme>(memeCount));
            var meme = memes.FirstOrDefault();

            if (meme == null) { return NotFound(); }

            return RedirectToAction("SingleMeme", new { id = meme.Id, commentPage = 1 });
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
            if (ModelState.IsValid)
            {
                string imagePath = memeFileService.Save(model.Image);
                Meme meme = new Meme
                {
                    Title = model.Title,
                    ImagePath = imagePath,
                    UserId = this.GetCurrentUserId()
                };
                await memeRepo.AddAsync(meme);
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteMeme(int id)
        {
            if (!(User.IsInRole("Admin") || await this.IsOwnedByCurrentUser(id, memeRepo)))
            {
                return Forbid();
            }
            var meme = await memeRepo.GetByIdAsync(id);
            if (meme == null) return NotFound();
            await memeRepo.DeleteAsync(meme);
            memeFileService.Delete(meme.ImagePath);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(string text, int memeId)
        {
            if (text.Length > 300) { return (StatusCode(400)); }

            var meme = await memeRepo.GetByIdAsync(memeId);
            if (meme == null) { return (StatusCode(400)); }

            var comment = new Comment
            {
                Text = text,
                Meme = meme,
                UserId = this.GetCurrentUserId()
            };
            await commentRepo.AddAsync(comment);

            return RedirectToAction("SingleMeme", new { id = memeId, commentPage = 1 });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!(User.IsInRole("Admin") || await this.IsOwnedByCurrentUser(id, commentRepo)))
            {
                return Forbid();
            }
            var comment = await commentRepo.GetByIdAsync(id);
            if (comment == null) return NotFound();
            await commentRepo.DeleteAsync(comment);
            return RedirectToAction("Index");
        }
    }
}
