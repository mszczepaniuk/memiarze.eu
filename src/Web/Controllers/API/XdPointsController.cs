using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Extensions;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Models.Interfaces;
using memiarzeEu.Specifications.XdPointSpec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace memiarzeEu.Controllers.API
{
    [Authorize]
    [ApiController]
    public abstract class XdPointsController<T1, T2> : ControllerBase
        where T1 : IPointable<T2>
        where T2 : XdPoint

    {
        protected readonly IAsyncRepository<T2> elementPointsRepo;
        protected readonly IAsyncRepository<T1> elementRepo;
        protected BaseXdPointUserIdAndMemeId<T2> specification;

        public XdPointsController(IAsyncRepository<T2> elementPointsRepo,
            IAsyncRepository<T1> elementRepo)
        {
            this.elementPointsRepo = elementPointsRepo;
            this.elementRepo = elementRepo;
        }

        [HttpPost("api/{controller}/{id}/award")]
        public virtual async Task<IActionResult> Award(int id)
        {
            var element = await elementRepo.GetByIdAsync(id);
            if (element == null) { return BadRequest(new { message = "Meme wasn't found" }); }

            var userPoint = await FindUserPoint(specification, id);
            if (userPoint != null) { return BadRequest(new { message = "User already awarded a point to this element" }); }

            var xdPointFactory = new XdPointFactory<T2>();

            var xdPoint = xdPointFactory.Create(this.GetCurrentUserId(), id) as T2;

            await elementPointsRepo.AddAsync(xdPoint);
            return Ok();
        }

        [HttpDelete("api/{controller}/{id}/remove")]
        public async virtual Task<IActionResult> Remove(int id)
        {
            var element = await elementRepo.GetByIdAsync(id);
            if (element == null) { return BadRequest(new { message = "Meme wasn't found" }); }

            var userPoint = await FindUserPoint(specification, id);
            if (userPoint == null) { return BadRequest(new { message = "User hasn't awarded a point to this element" }); }

            await elementPointsRepo.DeleteAsync(userPoint);
            return Ok();
        }

        protected async Task<T2> FindUserPoint(BaseXdPointUserIdAndMemeId<T2> specification, int id)
        {
            specification.UserId = this.GetCurrentUserId();
            specification.Id = id;
            var userPoint = await elementPointsRepo.GetAsync(specification);
            return userPoint.FirstOrDefault();
        }
    }
}