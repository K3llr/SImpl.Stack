using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App;
using Novicell.App.AppBuilders;
using Novicell.App.DependencyInjection.Configuration;
using SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration;
using SImpl.Modules;
using SimpleInjector;

namespace SImpl.DotNetStack.GenericHost.DependencyInjection
{
    public class GenericHostDependencyInjectionModule : INovicellModule, IHostConfigureModule, IServicesCollectionConfigureModule
    {
        public static GenericHostDependencyInjectionConfig Config { get; private set;  }
    
        public GenericHostDependencyInjectionModule(GenericHostDependencyInjectionConfig config)
        {
            Config = config;
        }

        public string Name => ".NET 5 ServiceCollection integration stack module ";

        public void Configure(INovicellAppBuilder appBuilder)
        {
            appBuilder.UseDependencyInjection(Config.DependencyInjectionConfig.Container);
        }

        public void Init()
        {
        }
        
        public void ConfigureHost(IHost host)
        {
            host.UseSimpleInjector(Config.DependencyInjectionConfig.Container);
            
            Config.DependencyInjectionConfig.Container.Verify();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimpleInjector(Config.DependencyInjectionConfig.Container, options =>
            {
                foreach (var optionConfigure in Config.OptionConfigures)
                {
                    optionConfigure?.Invoke(options);
                }
            });
        }
    }
}