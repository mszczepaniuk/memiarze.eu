using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.CommentSpec
{
    public class PageOfCommentsOrderedByDateSpec : PageOfCommentsSpec
    {
        public PageOfCommentsOrderedByDateSpec(int page) : base(page)
        {
            OrderByDescending = x => x.CreationDate;
        }
    }
}
