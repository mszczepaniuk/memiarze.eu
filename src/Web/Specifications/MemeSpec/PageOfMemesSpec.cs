using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.MemeSpec
{
    public class PageOfMemesSpec : BaseSpecification<Meme>
    {
        public PageOfMemesSpec(int page)
        {
            Page = page;
            ResultsPerPage = 10;
            Includes.Add(x => x.XdPoints);
            Includes.Add(x => x.User);
        }
    }
}
