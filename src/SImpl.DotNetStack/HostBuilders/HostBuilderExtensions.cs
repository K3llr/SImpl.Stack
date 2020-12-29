using System;
using Microsoft.Extensions.Hosting;
using Novicell.DotNetStack.Core;

namespace Novicell.DotNetStack.HostBuilders
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddDotNetStack(this IHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> stackHostBuilder = null)
        {
            var runtime = DotNetStackRuntime.Init(hostBuilder);
            return new DotNetStackHostBuilder(runtime, stackHostBuilder);
        }
    }
}