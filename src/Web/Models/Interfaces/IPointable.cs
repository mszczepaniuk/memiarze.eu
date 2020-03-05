using System.Collections.Generic;

namespace memiarzeEu.Models.Interfaces
{
    public interface IPointable<T>
        where T : XdPoint
    {
        public List<T> XdPoints { get; set; }
    }
}
