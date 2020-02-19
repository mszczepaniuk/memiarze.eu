using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class XdPointFactory<T> where T : XdPoint
    {
        public XdPoint Create(string userId, int id)
        {
            Type inputType = typeof(T);

            if (inputType.Equals(typeof(MemeXdPoint)))
            {
                return new MemeXdPoint { UserId = userId, MemeId = id };
            }
            else if (inputType.Equals(typeof(CommentXdPoint)))
            {
                return new CommentXdPoint { UserId = userId, CommentId = id };
            }
            else
            {
                throw new ArgumentException("Can't return abstract type");
            }
        }
    }
}
