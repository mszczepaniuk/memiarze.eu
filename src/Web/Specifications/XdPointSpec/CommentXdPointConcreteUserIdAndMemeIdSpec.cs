using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.XdPointSpec
{
    public class CommentXdPointConcreteUserIdAndMemeIdSpec : BaseSpecification<CommentXdPoint>
    {
        private readonly string userId;
        private readonly int commentId;

        public CommentXdPointConcreteUserIdAndMemeIdSpec(string userId, int commentId)
        {
            this.userId = userId;
            this.commentId = commentId;
        }
        public override List<Expression<Func<CommentXdPoint, bool>>> Criterias => new List<Expression<Func<CommentXdPoint, bool>>> { x => x.UserId == userId, x => x.CommentId == commentId };
    }
}
