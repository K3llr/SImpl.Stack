using System;
using Microsoft.Extensions.Hosting;

namespace SImpl.DotNetStack.HostBuilders
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddDotNetStack(this IHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> stackHostBuilder = null)
        {
            return hostBuilder.AddDotNetStack(Array.Empty<string>(), stackHostBuilder);
        }
        
        public static IHostBuilder AddDotNetStack(this IHostBuilder hostBuilder, string[] args, Action<IDotNetStackHostBuilder> stackHostBuilder = null)
        {
            return DotNetStack.Init(hostBuilder, args, stackHostBuilder);
        }
    }
}