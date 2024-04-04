using DuckC2.Server.Abstractions.Models;

namespace DuckC2.Server.Abstractions.Factories
{
    public interface IListenerFactory
    {
        string Name { get; }
       
        IListener Create(dynamic args);

    }
}
