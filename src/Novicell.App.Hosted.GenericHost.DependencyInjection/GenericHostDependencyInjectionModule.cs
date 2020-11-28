using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.AppBuilders;
using Novicell.App.DependencyInjection.Configuration;
using Novicell.App.Hosted.GenericHost.DependencyInjection.Configuration;
using Novicell.App.Hosted.Modules;
using SimpleInjector;

namespace Novicell.App.Hosted.GenericHost.DependencyInjection
{
    public class GenericHostDependencyInjectionModule : INovicellModule, IHostObjectConfigureModule, IServicesCollectionConfigureModule
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