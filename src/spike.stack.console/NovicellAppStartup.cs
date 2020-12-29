using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Configuration;
using Novicell.App.DependencyInjection.Configuration;
using SImpl.DotNetStack.App.Extensions;
using SImpl.DotNetStack.GenericHost.DependencyInjection;
using SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration;
using SimpleInjector;
using spike.stack.app.Application;
using spike.stack.app.Domain;
using spike.stack.module;

namespace spike.stack.console
{
    public class NovicellAppStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNovicellConsoleApp(consoleApp =>
            {                
                consoleApp.UseHybridTestModule();
                consoleApp.Use<LegacyStackModule>();
                
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
            });
        }
    }
}