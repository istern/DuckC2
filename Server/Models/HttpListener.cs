using DuckC2.Server.Abstractions.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;

namespace DuckC2.Server.Models
{
    public class HttpListener : IListener
    {
        private CancellationTokenSource tokenSource;
        public string Name { get; }
        public string BindPort { get; }
        private HttpListenerImplantHandler implantHandler;
        public HttpListener(string name, string bindPort)
        {
            Name = name;
            BindPort = bindPort;
            tokenSource = new CancellationTokenSource();
            implantHandler = new HttpListenerImplantHandler();
        }


        public async Task Start()
        {
            var hostBuilder = new HostBuilder()
                    .ConfigureWebHostDefaults(host =>
                    {
                        host.UseUrls($"http://0.0.0.0:{BindPort}");
                        host.Configure(ConfigureApp);
                        host.ConfigureServices(ConfigureServices);
                    });

            var host = hostBuilder.Build();


            await host.RunAsync(tokenSource.Token);
        }

        private void ConfigureServices(IServiceCollection services)
        {
        }


        private void ConfigureApp(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapGet("/", async Task<Results<Ok<Duckling>, NotFound>> (HttpContext context) =>
                    await implantHandler.HandleCheckin(context) is Duckling duckling ? TypedResults.Ok(duckling) : TypedResults.NotFound());
            });
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
    }
}
