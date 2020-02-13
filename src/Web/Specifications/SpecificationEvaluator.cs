﻿using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.Page > 0 && specification.ResultsPerPage > 0)
            {
                query = query.Take(specification.ResultsPerPage).Skip((specification.Page - 1) * specification.ResultsPerPage);
            }

            return query;
        }
    }
}
