using DuckC2.Server.Abstractions.Models;

namespace DuckC2.Server.Models
{
    public class HttpListener : IListener
    {
        private CancellationTokenSource tokenSource;
        public string Name { get; }
        public string BindPort { get; }
        public HttpListener(string name,string bindPort)
        {
            Name = name;
            BindPort = bindPort;
            tokenSource = new CancellationTokenSource();
        }
      

        public async Task Start()
        {
            var hostBuilder = new HostBuilder()
                    .ConfigureWebHostDefaults(host =>
                    {
                        host.UseUrls($"http://0.0.0.0:{BindPort}");
                        host.Configure(ConfigureApp);
                    });

            var host = hostBuilder.Build();

           await host.RunAsync(tokenSource.Token);
        }

        

        private void ConfigureApp(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapControllerRoute("/", "/", new { controller = "HttpListener", action = "HandleImplant" });
            });
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
    }
}
