﻿using memiarzeEu.Interfaces;
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
            IAsyncRepository<Comment> elementRepo, 
            IXdPointFactory<CommentXdPoint> xdPointFactory) :base(elementPointsRepo, elementRepo, xdPointFactory)
        {
            specification = new CommentXdPointConcreteUserIdAndMemeIdSpec();
        }
    }
}
