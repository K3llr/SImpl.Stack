using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Application.Builders;
using SImpl.Modules;

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