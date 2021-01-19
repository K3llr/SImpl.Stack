using System;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.GenericHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDotNetStackGenericApp(this IApplicationBuilder applicationBuilder, Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            applicationBuilder.Runtime.Container.Resolve<IDotNetStackApplicationBuilder>().Configure(configureDelegate);
        }
    }
}