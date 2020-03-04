using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Specifications.XdPointSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Controllers.API
{
    public class MemeXdPointController : XdPointsController<Meme, MemeXdPoint>
    {
        public MemeXdPointController(IAsyncRepository<MemeXdPoint> elementPointsRepo,
            IAsyncRepository<Meme> elementRepo,
            IXdPointFactory<MemeXdPoint> xdPointFactory) : base(elementPointsRepo, elementRepo, xdPointFactory)
        {
            specification = new MemeXdPointConcreteUserIdAndMemeIdSpec();
        }
    }
}
