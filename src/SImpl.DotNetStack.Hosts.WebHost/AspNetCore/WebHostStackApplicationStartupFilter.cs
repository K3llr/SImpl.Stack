using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Hosts.WebHost.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Extensions;

namespace SImpl.DotNetStack.Hosts.WebHost.AspNetCore
{
    public class WebHostStackApplicationStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                var moduleManager = DotNetStackRuntimeServices.Current.BootContainer.Resolve<IBootSequenceFactory>();
                moduleManager.New().ForEach<IAspNetApplicationModule>(module => module.Configure(builder, builder.ApplicationServices.GetService<IWebHostEnvironment>()));
                
                next(builder);
            };
        }
    }
}