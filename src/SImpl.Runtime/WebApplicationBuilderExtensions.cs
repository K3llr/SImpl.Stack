using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;

namespace SImpl.Runtime;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder SImplify(
        this WebApplicationBuilder webApplicationBuilder,
        Action<ISImplHostBuilder> simpl = null)
    {
        return webApplicationBuilder.SImplify(Array.Empty<string>(), simpl);
    }

    public static WebApplicationBuilder SImplify(
        this WebApplicationBuilder webApplicationBuilder, 
        string[] args,
        Action<ISImplHostBuilder> simpl = null)
    {
        return webApplicationBuilder.SImplify(args, null, simpl);
    }

    public static WebApplicationBuilder SImplify(
        this WebApplicationBuilder webApplicationBuilder, 
        string[] args, 
        ILogger logger, 
        Action<ISImplHostBuilder> simpl = null)
    {
        return SImply.Boot(webApplicationBuilder, args, logger, simpl);
    }
}