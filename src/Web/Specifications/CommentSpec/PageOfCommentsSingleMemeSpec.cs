using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.CommentSpec
{
    public class PageOfCommentsSingleMemeSpec : PageOfCommentsOrderedByDateSpec
    {
        protected readonly int memeId;

        public PageOfCommentsSingleMemeSpec(int page, int memeId) : base(page)
        {
            this.memeId = memeId;
        }

        public override List<Expression<Func<Comment, bool>>> Criterias => new List<Expression<Func<Comment, bool>>> { x => x.MemeId == memeId };
    }
}
