using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Extensions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.GenericHost.Application
{
    public class GenericHostStackApplication : IDotNetStackApplication
    {
        private readonly IDotNetStackRuntime _runtime;
        private readonly IStartup _startup;

        public GenericHostStackApplication(IDotNetStackRuntime runtime, IStartup startup)
        {
            _runtime = runtime;
            _startup = startup;
        }
        
        public void ConfigureService(IServiceCollection services)
        {
            _startup.ConfigureServices(services);
        }

        public Task StartAsync()
        {
            // TODO: Build app stack and wrap 
            
            _startup.Configure(new DotNetStackApplicationBuilder(_runtime));
                
            // TODO configure and start applications modules
            
            foreach (var moduleContext in _runtime.ModuleManager.ModuleInfos)
            {
                if (ModuleState.Attached.Equals(moduleContext.State))
                {
                    /*if (moduleContext.Module is IApplicationConfigureModule appModule)
                    {
                        appModule.ConfigureApplication();
                    }*/
                }
            }
            
            // TODO: Maybe something like
            // TODO: _runtime.ModuleManager.ChangeState<IApplicationModule>(module => module.StartAsync(), ModuleState.Started) 
            // TODO: OR
            // TODO: BootManager.StartApplications();
            
            _runtime.ModuleManager.EnabledModules.ForEach<IApplicationModule>(module =>
            {
                module.StartAsync();
            });
           
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _runtime.ModuleManager.EnabledModules.ForEach<IApplicationModule>(module =>
            {
                module.StopAsync();
            });
                
            return Task.CompletedTask;
        }
    }
}