using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Specifications.XdPointSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Controllers.API
{
    public class CommentXdPointController : XdPointsController<Comment, CommentXdPoint>
    {
        public CommentXdPointController(IAsyncRepository<CommentXdPoint> elementPointsRepo,
            IAsyncRepository<Comment> elementRepo) :base(elementPointsRepo, elementRepo)
        {
            specification = new CommentXdPointConcreteUserIdAndMemeIdSpec();
        }
    }
}
