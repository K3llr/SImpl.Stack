using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;

namespace SImpl.Runtime;

public static class WebApplicationBuilderExtensions
{
    public static ISImplHostBuilder SImplify(this WebApplicationBuilder hostBuilder,
        Action<ISImplHostBuilder> simpl = null)
    {
        return hostBuilder.SImplify(Array.Empty<string>(), simpl);
    }

    public static ISImplHostBuilder SImplify(this WebApplicationBuilder hostBuilder, string[] args,
        Action<ISImplHostBuilder> simpl = null)
    {
        return hostBuilder.SImplify(args, null, simpl);
    }

    public static ISImplHostBuilder SImplify(this WebApplicationBuilder hostBuilder, string[] args, ILogger logger,
        Action<ISImplHostBuilder> simpl = null)
    {
        return SImply.Boot(hostBuilder, args, logger, simpl);
    }
}