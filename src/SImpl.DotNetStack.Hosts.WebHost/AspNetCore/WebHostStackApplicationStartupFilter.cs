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
        private readonly IBootSequenceFactory _bootSequenceFactory;

        public WebHostStackApplicationStartupFilter(IBootSequenceFactory bootSequenceFactory)
        {
            _bootSequenceFactory = bootSequenceFactory;
        }
        
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                _bootSequenceFactory.New().ForEach<IAspNetApplicationModule>(module => module.Configure(builder, builder.ApplicationServices.GetService<IWebHostEnvironment>()));
                
                next(builder);
            };
        }
    }
}