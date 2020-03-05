using memiarzeEu.Models;

namespace memiarzeEu.Interfaces
{
    public interface IXdPointFactory<T>
    {
        public XdPoint Create(string userId, int id);
    }
}
