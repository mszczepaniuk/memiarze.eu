using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Models.Interfaces;
using memiarzeEu.Specifications;
using Microsoft.AspNetCore.Mvc;
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

        public async static Task<bool> IsOwnedByCurrentUser<T>(this ControllerBase controllerBase, int elementId, IAsyncRepository<T> repo) where T : BaseEntity, IOwnedByUser
        {
            var result = await repo.GetAsync(new ConcreteUserIdAndElementIdSpec<T>(controllerBase.GetCurrentUserId(), elementId));
            return result.Any();
        }
    }
}
