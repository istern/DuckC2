namespace DuckC2.Server.Abstractions.Models
{
    public interface IListener
    {
        public string Name { get; }
        public Task Start();
        public void Stop();
    }
}