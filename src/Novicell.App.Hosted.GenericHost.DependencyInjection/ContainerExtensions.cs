using System;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Integration.ServiceCollection;

namespace Novicell.App.Hosted.GenericHost.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void RegisterHostedService<THostedService>(this Container container)
            where THostedService : class, IHostedService
        {
            if (GenericHostDependencyInjectionModule.Config is null)
            {
                throw new InvalidOperationException("No generic host configured: di.UseGenericHost()");
            }
            GenericHostDependencyInjectionModule.Config?.AddOptions(options =>
            {
                options.AddHostedService<THostedService>();
            });
        }
    }
}