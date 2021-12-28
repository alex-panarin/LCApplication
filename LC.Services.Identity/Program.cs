using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace LC.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel(options =>
                     {
                         options.ListenAnyIP(
                             80, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1AndHttp2; }
                         );
                         options.ListenAnyIP(
                             5001, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; }
                         );
                     });

                    webBuilder.UseDefaultServiceProvider(option => option.ValidateScopes = false);
                    webBuilder.UseStartup<Startup>();

                });
    }
}
