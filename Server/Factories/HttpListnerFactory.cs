using DuckC2.Server.Abstractions.Factories;
using DuckC2.Server.Abstractions.Models;
using System.Net;
using HttpListener = DuckC2.Server.Models.HttpListener;

namespace DuckC2.Server.Factories
{
    public class HttpListnerFactory : IListenerFactory
    {

        public HttpListnerFactory()
        {
        }

        public string Name => "HTTP Listener";

        public  Type LType => typeof(HttpListener);

        public IListener Create(dynamic args)
        {
            var listener = new HttpListener(args.Name, args.BindingPort);
           
            return listener;
        }

      
    }
}
