using System;
using Microsoft.AspNetCore.Builder;
using Novicell.App.AppBuilders;
using Novicell.App.Web.Configuration;

namespace Novicell.App.Web.Extensions.Configuration
{
    public static class ApplicationBuilderExtension
    {
        public static void UseWebStackApp(this IApplicationBuilder applicationBuilder, Action<IWebAppBuilder> configure)
        {
            applicationBuilder.UseNovicellWebApp(configure);
            
            WebBootManager.Boot(applicationBuilder);
        }
    }
}