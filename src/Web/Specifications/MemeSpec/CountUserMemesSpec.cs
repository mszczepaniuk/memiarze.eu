using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class CountUserMemesSpec : BaseSpecification<Meme>
    {
        private readonly string userId;

        public CountUserMemesSpec(string userId)
        {
            this.userId = userId;
        }
        public override List<Expression<Func<Meme, bool>>> Criterias => new List<Expression<Func<Meme, bool>>> { x => x.UserId == userId };
    }
}
