using System.Threading.Tasks;
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
        public static void UseApplicationTestModule(this ISImplApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAppModule<ApplicationTestModule>();
        }
    }
}