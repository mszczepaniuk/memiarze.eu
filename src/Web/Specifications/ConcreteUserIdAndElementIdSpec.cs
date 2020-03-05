using memiarzeEu.Models;
using memiarzeEu.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace memiarzeEu.Specifications
{
    public class ConcreteUserIdAndElementIdSpec<T> : BaseSpecification<T>
        where T : BaseEntity, IOwnedByUser
    {
        private readonly string userId;
        private readonly int elementId;

        public ConcreteUserIdAndElementIdSpec(string userId, int elementId)
        {
            this.userId = userId;
            this.elementId = elementId;
        }

        public override List<Expression<Func<T, bool>>> Criterias => new List<Expression<Func<T, bool>>> { x => x.UserId == userId, x => x.Id == elementId };
    }
}