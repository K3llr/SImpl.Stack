using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Application.Builders;
using SImpl.Modules;
using SImpl.Runtime;

namespace spike.stack.module
{
    public class ApplicationTestModule : IApplicationModule
    {
        public void Configure(ISImplApplicationBuilder builder)
        {
      
        }

        public string Name => nameof(ApplicationTestModule);
            
        public Task StartAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(IHost host)
        {
            return Task.CompletedTask;
        }
    }

    public static partial class AppBuilderExtension
    {
        public static void UseApplicationTestModule(this ISImplApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAppModule<ApplicationTestModule>();
        }
    }
}