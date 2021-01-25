using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.Modules;
using SImpl.DotNetStack.Modules;

namespace spike.stack.module
{
    public class WebHostApplicationTestModule : IWebHostApplicationModule
    {
        public string Name => nameof(WebHostApplicationTestModule);
        public void Configure(IWebHostApplicationBuilder builder)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
    
    public static partial class AppBuilderExtension
    {
        public static void UseWebApplicationTestModule(this IWebHostApplicationBuilder hostApplicationBuilder)
        {
            //applicationBuilder.UseWeb<WebApplicationTestModule>();
        }
    }
}