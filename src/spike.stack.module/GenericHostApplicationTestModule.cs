using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Application.Builders;
using SImpl.Modules;

namespace spike.stack.module
{
    public class GenericHostApplicationTestModule : IGenericHostApplicationModule
    {
        public void Configure(IGenericHostApplicationBuilder builder)
        {
            
        }

        public Task StartAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public string Name => nameof(GenericHostApplicationTestModule);
    }
    
    public static partial class AppBuilderExtension
    {
        public static void UseGenericHostApplicationTestModule(this IGenericHostApplicationBuilder applicationBuilder)
        {
            applicationBuilder.AttachNewGenericHostAppModuleOrGetConfigured(() => new GenericHostApplicationTestModule());
        }
    }
}