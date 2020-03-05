using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class PageOfMemesUserTopSpec : PageOfMemesSpec
    {
        private readonly string userId;

        public PageOfMemesUserTopSpec(string userId, int page) : base(page)
        {
            OrderByDescending = x => x.XdPoints.Count;
            this.userId = userId;
        }

        public override List<Expression<Func<Meme, bool>>> Criterias => new List<Expression<Func<Meme, bool>>> { x => x.UserId == userId };
    }
}
