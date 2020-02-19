using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications
{
    public class RandomElementSpec<T> : BaseSpecification<T>
    {
        public RandomElementSpec(int elementCount)
        {
            ResultsPerPage = 1;
            var rnd = new Random();
            Page = rnd.Next(1, elementCount);
        }
    }
}
