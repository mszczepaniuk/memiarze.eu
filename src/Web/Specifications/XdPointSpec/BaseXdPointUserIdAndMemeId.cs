using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Specifications.XdPointSpec
{
    public abstract class BaseXdPointUserIdAndMemeId<T> : BaseSpecification<T>
    {
        public string UserId { get; set; }
        public int Id { get; set;  }

        public BaseXdPointUserIdAndMemeId(string userId, int id)
        {
            UserId = userId;
            Id = id;
        }

        public BaseXdPointUserIdAndMemeId()
        {

        }
    }
}
