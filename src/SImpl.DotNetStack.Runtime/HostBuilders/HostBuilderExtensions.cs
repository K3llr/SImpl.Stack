using System;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.Runtime.HostBuilders
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddDotNetStack(this IHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> stackHostBuilder = null)
        {
            return hostBuilder.AddDotNetStack(Array.Empty<string>(), stackHostBuilder);
        }
        
        public static IHostBuilder AddDotNetStack(this IHostBuilder hostBuilder, string[] args, Action<IDotNetStackHostBuilder> stackHostBuilder = null)
        {
            return DotNetStackRuntime.Init(hostBuilder, args, stackHostBuilder);
        }
    }
}