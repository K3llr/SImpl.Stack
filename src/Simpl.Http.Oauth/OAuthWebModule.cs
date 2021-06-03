using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simpl.DependencyInjection;
using SImpl.Host.Builders;
using SImpl.Modules;
using Simpl.Oauth.Api;
using Simpl.Oauth.Configuration;
using Simpl.Oauth.Services;

namespace Simpl.Oauth
{
    public class OAuthWebModule : IServicesCollectionConfigureModule, IHostBuilderConfigureModule
    {
        public OAuthWebConfig WebConfig { get; set; }

        public OAuthWebModule(OAuthWebConfig webConfig)
        {
            WebConfig = webConfig;
        }

     

        public string Name { get; }
        public void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            var appBuilder = (ISImplHostBuilder) hostBuilder;
            appBuilder.UseOAuth(oauthCfg => { oauthCfg.SetPublicSigningKey(WebConfig.PublicSigningKey); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(WebConfig);

            if (WebConfig.IsServerEnabled)
            {
                services.AddScoped(typeof(IOAuthClientStorage), WebConfig.ServerConfig.ClientStorage);
                services.AddScoped(typeof(IOAuthRefreshTokenStorage), WebConfig.ServerConfig.RefreshTokenStorage);
                services.AddScoped(typeof(IOAuthUserProvider), WebConfig.ServerConfig.UserProvider);
                services.AddScoped<IOAuthClientService, OAuthClientService>();
                services.AddScoped<IOAuthRefreshTokenService, OAuthRefreshTokenService>();
                services.AddScoped<IOAuthAccessTokenService, OAuthAccessTokenService>();
            }
        }
    }
}