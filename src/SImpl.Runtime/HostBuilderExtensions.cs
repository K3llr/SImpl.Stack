using System;
using Microsoft.Extensions.Hosting;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder SImplify(this IHostBuilder hostBuilder, Action<ISImplHostBuilder> simpl = null)
        {
            return hostBuilder.SImplify(Array.Empty<string>(), simpl);
        }
        
        public static IHostBuilder SImplify(this IHostBuilder hostBuilder, string[] args, Action<ISImplHostBuilder> simpl = null)
        {
            return SImply.Boot(hostBuilder, args, simpl);
        }
    }
}