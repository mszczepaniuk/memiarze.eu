using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models.Interfaces
{
    public interface IPointable<T>
        where T : XdPoint
    {
        public List<T> XdPoints { get; set; }
    }
}
