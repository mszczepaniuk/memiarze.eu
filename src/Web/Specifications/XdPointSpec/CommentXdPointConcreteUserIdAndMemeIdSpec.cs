using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.XdPointSpec
{
    public class CommentXdPointConcreteUserIdAndMemeIdSpec : BaseXdPointUserIdAndMemeId<CommentXdPoint>
    {
        public CommentXdPointConcreteUserIdAndMemeIdSpec(string userId, int commentId) : base(userId, commentId)
        {
            
        }

        public CommentXdPointConcreteUserIdAndMemeIdSpec()
        {

        }

        public override List<Expression<Func<CommentXdPoint, bool>>> Criterias => new List<Expression<Func<CommentXdPoint, bool>>> { x => x.UserId == UserId, x => x.CommentId == Id };
    }
}
