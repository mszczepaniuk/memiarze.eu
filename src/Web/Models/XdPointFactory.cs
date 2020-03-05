using memiarzeEu.Interfaces;
using System;

namespace memiarzeEu.Models
{
    public class XdPointFactory<T> : IXdPointFactory<T> where T : XdPoint
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
