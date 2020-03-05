using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class PageOfMemesSpec : BaseSpecification<Meme>
    {
        public PageOfMemesSpec(int page)
        {
            Page = page;
            ResultsPerPage = 10;
        }

        public override List<Expression<Func<Meme, object>>> Includes => new List<Expression<Func<Meme, object>>> { x => x.User, x => x.XdPoints };
    }
}
