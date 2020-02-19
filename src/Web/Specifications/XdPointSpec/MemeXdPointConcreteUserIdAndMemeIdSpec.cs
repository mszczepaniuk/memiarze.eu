using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.XdPointSpec
{
    public class MemeXdPointConcreteUserIdAndMemeIdSpec : BaseSpecification<MemeXdPoint>
    {
        private readonly string userId;
        private readonly int memeId;

        public MemeXdPointConcreteUserIdAndMemeIdSpec(string userId, int memeId)
        {
            this.userId = userId;
            this.memeId = memeId;
        }
        public override List<Expression<Func<MemeXdPoint, bool>>> Criterias => new List<Expression<Func<MemeXdPoint, bool>>> { x => x.UserId == userId, x => x.MemeId == memeId };
    }
}
