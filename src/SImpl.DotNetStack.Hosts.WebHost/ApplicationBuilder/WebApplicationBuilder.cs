using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder
{
    public class WebApplicationBuilder : IWebApplicationBuilder
    {
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }

        public IApplicationBuilder New()
        {
            throw new NotImplementedException();
        }

        public void Use<TModule>(Func<TModule> factory) where TModule : IApplicationModule
        {
            throw new NotImplementedException();
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) where TModule : IApplicationModule
        {
            throw new NotImplementedException();
        }

        public void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            throw new NotImplementedException();
        }

        IDotNetStackApplication IDotNetStackApplicationBuilder.Build()
        {
            throw new NotImplementedException();
        }

        public RequestDelegate Build()
        {
            throw new NotImplementedException();
        }

        public IServiceProvider ApplicationServices { get; set; }
        public IFeatureCollection ServerFeatures { get; }
        public IDictionary<string, object?> Properties { get; }

        
    }
}