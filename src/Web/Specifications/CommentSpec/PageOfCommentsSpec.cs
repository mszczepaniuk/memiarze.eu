using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Specifications.CommentSpec
{
    public class PageOfCommentsSpec : BaseSpecification<Comment>
    {
        public PageOfCommentsSpec(int page)
        {
            Page = page;
            ResultsPerPage = 20;
        }

        public override List<Expression<Func<Comment, object>>> Includes => new List<Expression<Func<Comment, object>>> { x => x.User, x => x.XdPoints };
    }
}
