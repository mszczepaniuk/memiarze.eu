using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class SingleMemeSpec : BaseSpecification<Meme>
    {
        private readonly int memeId;

        public SingleMemeSpec(int memeId)
        {
            this.memeId = memeId;
        }
        public override List<Expression<Func<Meme, object>>> Includes => new List<Expression<Func<Meme, object>>> { x => x.User, x => x.XdPoints };
        public override List<Expression<Func<Meme, bool>>> Criterias => new List<Expression<Func<Meme, bool>>> { x => x.Id == memeId };
    }
}
