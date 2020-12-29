using System;
using Microsoft.AspNetCore.Builder;
using Novicell.App.AppBuilders;
using Novicell.App.Web;
using Novicell.App.Web.Configuration;

namespace SImpl.DotNetStack.WebHost.Configuration
{
    public static class ApplicationBuilderExtension
    {
        public static void UseWebStackApp(this IApplicationBuilder applicationBuilder, Action<IWebAppBuilder> configure)
        {
            Console.WriteLine("UseWebStackApp");

            applicationBuilder.UseNovicellWebApp(configure);
            
            WebBootManager.Boot(applicationBuilder);
        }
    }
}