using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Specifications.CommentSpec
{
    public class CountCommentsOnMeme : BaseSpecification<Comment>
    {
        private readonly int memeId;

        public CountCommentsOnMeme(int memeId)
        {
            this.memeId = memeId;
        }
        public override List<Expression<Func<Comment, bool>>> Criterias => new List<Expression<Func<Comment, bool>>> { x => x.MemeId == memeId };
    }
}
