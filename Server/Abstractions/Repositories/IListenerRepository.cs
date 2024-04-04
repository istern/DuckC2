using DuckC2.Server.Abstractions.Models;

namespace DuckC2.Server.Abstractions.Repositories
{
    public interface IListenerRepository
    {
        void Add(IListener listener);
        IEnumerable<IListener> Get();
        IListener Get(string name);
        void Remove(IListener listener);
    }
}