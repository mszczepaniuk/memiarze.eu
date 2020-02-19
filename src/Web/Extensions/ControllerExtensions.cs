using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace memiarzeEu.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetCurrentUserId(this ControllerBase controllerBase)
        {
            var user = controllerBase.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string userId = user?.Value;
            return userId;
        }
    }
}
