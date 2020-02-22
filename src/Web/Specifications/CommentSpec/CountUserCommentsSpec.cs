using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.CommentSpec
{
    public class CountUserCommentsSpec : BaseSpecification<Comment>
    {
        private readonly string userId;

        public CountUserCommentsSpec(string userId)
        {
            this.userId = userId;
        }
        public override List<Expression<Func<Comment, bool>>> Criterias => new List<Expression<Func<Comment, bool>>> { x => x.UserId == userId };
    }
}
