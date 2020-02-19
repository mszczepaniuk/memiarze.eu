using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.XdPointSpec
{
    public class MemeXdPointConcreteUserIdAndMemeIdSpec : BaseXdPointUserIdAndMemeId<MemeXdPoint>
    {
        public MemeXdPointConcreteUserIdAndMemeIdSpec(string userId, int memeId) : base(userId, memeId)
        {

        }
        public MemeXdPointConcreteUserIdAndMemeIdSpec()
        {

        }

        public override List<Expression<Func<MemeXdPoint, bool>>> Criterias => new List<Expression<Func<MemeXdPoint, bool>>> { x => x.UserId == UserId, x => x.MemeId == Id };
    }
}
