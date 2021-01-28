using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SImpl.Hosts.WebHost;
using SImpl.Runtime;

namespace SImpl.Templates.WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .SImplify(args, simpl => 
                {
                    simpl.ConfigureWebHostStackApp(host =>
                    {
                        host.ConfigureApplication(app =>
                        {

                        });
                    });
                });
    }
}
