using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SImpl.Hosts.WebHost;
using SImpl.Runtime;
using spike.stack.module;

namespace spike.stack.web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHost =>
                {
                    webHost.UseStartup<Startup>();
                })
                .SImplify(args, simpl =>
                {
                    simpl.Use<RootModule>();
                    simpl.ConfigureWebHostStackApp(webHost =>
                    {
                        webHost.UseStartup<Startup>();
                        webHost.ConfigureApplication(app =>
                        {
                            app.UseWebHostAppModule<GreetingsWebModule>();
                            app.UseAppModule<ApplicationTestModule>();
                        });
                    });
                })
                .Build()
                .RunAsync();
        }
    }
}
