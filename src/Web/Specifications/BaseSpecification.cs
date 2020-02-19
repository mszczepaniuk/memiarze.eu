using memiarzeEu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public virtual List<Expression<Func<T, bool>>> Criterias => new List<Expression<Func<T, bool>>>();

        public virtual List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

        public virtual Expression<Func<T, object>> OrderBy { get; protected set; }

        public virtual Expression<Func<T, object>> OrderByDescending { get; protected set; }

        public virtual int Page { get; protected set; }

        public virtual int ResultsPerPage { get; protected set; }
    }
}
