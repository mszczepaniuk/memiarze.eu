using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Interfaces
{
    public interface IXdPointFactory<T>
    {
        public XdPoint Create(string userId, int id);
    }
}
