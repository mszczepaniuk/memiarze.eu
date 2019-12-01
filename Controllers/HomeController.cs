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

namespace memiarzeEu.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult AddMeme()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
