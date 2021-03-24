using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Hosts.WebHost.Modules;
using SImpl.Runtime.Core;
using SImpl.Runtime.Extensions;

namespace SImpl.Hosts.WebHost.AspNetCore
{
    public class WebHostApplicationStartupFilter : IStartupFilter
    {
        private readonly IBootSequenceFactory _bootSequenceFactory;

        public WebHostApplicationStartupFilter(IBootSequenceFactory bootSequenceFactory)
        {
            _bootSequenceFactory = bootSequenceFactory;
        }
        
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                _bootSequenceFactory.New().ForEach<IAspNetPreConfigureModule>(module => module.Configure(builder, builder.ApplicationServices.GetService<IWebHostEnvironment>()));
                
                next(builder);
                
                _bootSequenceFactory.New().ForEach<IAspNetPostConfigureModule>(module => module.Configure(builder, builder.ApplicationServices.GetService<IWebHostEnvironment>()));
            };
        }
    }
}