using System.Threading.Tasks;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Modules;

namespace spike.stack.module
{
    public class ApplicationTestModule : IApplicationModule
    {
        public void Configure(IDotNetStackApplicationBuilder builder)
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
        public static void UseApplicationTestModule(this IDotNetStackApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseStackAppModule<ApplicationTestModule>();
        }
    }
}