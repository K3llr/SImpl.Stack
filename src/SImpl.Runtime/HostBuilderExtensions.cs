using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder SImplify(
            this IHostBuilder hostBuilder, 
            Action<ISImplHostBuilder> simpl = null)
        {
            return hostBuilder.SImplify(Array.Empty<string>(), simpl);
        }
        
        public static IHostBuilder SImplify(
            this IHostBuilder hostBuilder, 
            string[] args, 
            Action<ISImplHostBuilder> simpl = null)
        {
            return hostBuilder.SImplify(args, null, simpl);
        }
        
        public static IHostBuilder SImplify(
            this IHostBuilder hostBuilder, 
            string[] args, 
            ILogger logger, 
            Action<ISImplHostBuilder> simpl = null)
        {
            return SImply.Boot(hostBuilder, args, logger, simpl);
        }
    }
}