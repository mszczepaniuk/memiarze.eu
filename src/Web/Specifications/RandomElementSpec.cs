using System;

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
