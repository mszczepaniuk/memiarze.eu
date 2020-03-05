using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Interfaces
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        public int Page { get; }
        public int ResultsPerPage { get; }
    }
}
