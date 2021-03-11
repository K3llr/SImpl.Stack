using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Hosts.WebHost;
using SImpl.Runtime;

namespace SImpl.Templates.WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = CreateLogger();

            try
            {
                await CreateHostBuilder(args, logger).Build().RunAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args, ILogger logger) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .SImplify(args, logger, simpl => 
                {
                    simpl.ConfigureWebHostStackApp(host =>
                    {
                        host.ConfigureApplication(app =>
                        {

                        });
                    });
                });
        
        private static ILogger CreateLogger()
        {
            using var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging
                    .AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss.fff ";
                    })
                    .SetMinimumLevel(LogLevel.Trace);
            });

            return loggerFactory.CreateLogger<SImply>();
        }
    }
}
