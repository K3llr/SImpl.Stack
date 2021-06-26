using Microsoft.Extensions.DependencyInjection;
using SImpl.Host.Builders;
using SImpl.Http.OAuth.Api;
using SImpl.Http.OAuth.Configuration;
using SImpl.Http.OAuth.Services;
using SImpl.Modules;
using SImpl.OAuth;

namespace SImpl.Http.OAuth
{
    public class OAuthWebModule : IServicesCollectionConfigureModule, IHostBuilderConfigureModule
    {
        public OAuthWebConfig WebConfig { get; set; }

        public OAuthWebModule(OAuthWebConfig webConfig)
        {
            WebConfig = webConfig;
        }

     

        public string Name { get; }
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseOAuth(oauthCfg => { oauthCfg.SetPublicSigningKey(WebConfig.PublicSigningKey); });
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