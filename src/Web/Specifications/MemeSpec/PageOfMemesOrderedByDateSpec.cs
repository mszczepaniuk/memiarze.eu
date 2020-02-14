using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class PageOfMemesOrderedByDateSpec : PageOfMemesSpec
    {
        public PageOfMemesOrderedByDateSpec(int page) : base(page)
        {
            OrderByDescending = x => x.CreationDate;
        }
    }
}
