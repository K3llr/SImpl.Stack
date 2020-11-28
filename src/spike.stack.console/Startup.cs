using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Configuration;
using Novicell.App.DependencyInjection.Configuration;
using Novicell.App.Hosted.GenericHost.DependencyInjection;
using Novicell.App.Hosted.GenericHost.DependencyInjection.Configuration;
using SimpleInjector;
using spike.stack.app.Application;
using spike.stack.app.Domain;

namespace spike.stack.console
{
    public class Startup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNovicellConsoleApp(consoleApp =>
            {                
                /*consoleApp.UseTest01Module();
                consoleApp.Use<Test02Module>();
                consoleApp.Use(() => new Test03Module());*/
                
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