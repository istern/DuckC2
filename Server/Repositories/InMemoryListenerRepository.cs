using DuckC2.Server.Abstractions.Models;
using DuckC2.Server.Abstractions.Repositories;
using System.Net.WebSockets;

namespace DuckC2.Server.Repositories
{
    public class InMemoryListenerRepository : IListenerRepository
    {

        public InMemoryListenerRepository()
        {
            Listeners = new List<IListener>(); 
        }

        public List<IListener> Listeners { get; private set; }

        public void Add(IListener listener)
        {
            if (Listeners.Count(x => x.Name.Equals(listener.Name, StringComparison.InvariantCultureIgnoreCase)) > 0)
                throw new Exception($"Listener with name {listener.Name} already exists");
            Listeners.Add(listener);
        }

        public IEnumerable<IListener> Get()
        {
            return Listeners;
        }

        public IListener Get(string name)
        {
            return Listeners.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Remove(IListener listener)
        {
            Listeners.Remove(listener);
        }
    }
}
