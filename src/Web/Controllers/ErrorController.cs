using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.ViewModels.Error;
using memiarzeEu.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IConfiguration configuration;

        public ErrorController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [Route("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var vm = new GenericErrorViewModel
            {
                ErrorCode = statusCode,
                ErrorImagePath = configuration.GetValue<string>("ErrorImages:Generic")
            };
            return View("GenericError", vm);
        }
    }
}