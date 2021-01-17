using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;
using spike.stack.module;

namespace spike.stack.console
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IDotNetStackApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use<TestStackedApplicationModule>();
            
            /*stackBuilder.UseNovicellConsoleApp(consoleApp =>
            {
               
                
                //consoleApp.UseHybridTestModule();
                //consoleApp.Use<LegacyStackModule>();

                consoleApp.UseDependencyInjection(di =>
                {
                    di.UseGenericHost();
                    
                    di.RegisterServices(container =>
                    {
                        container.RegisterHostedService<GreetingHostedService>();
                        
                        container.Register<IGreetingAppService, GreetingAppService>(Lifestyle.Singleton);
                        container.Register<IGreetingService, SpanishGreetingService>(Lifestyle.Singleton);
                    });
                });
            });*/
        }
    }
}