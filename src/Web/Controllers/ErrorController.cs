using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace memiarzeEu.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.ErrorStatusCode = statusCode;
            return View("GenericError");
        }
    }
}