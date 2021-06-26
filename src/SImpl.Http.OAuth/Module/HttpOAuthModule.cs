using Microsoft.Extensions.DependencyInjection;
using SImpl.Host.Builders;
using SImpl.Http.OAuth.Services;
using SImpl.Modules;
using SImpl.OAuth;

namespace SImpl.Http.OAuth.Module
{
    public class HttpOAuthModule : IServicesCollectionConfigureModule, IHostBuilderConfigureModule
    {
        public HttpOAuthConfig Config { get; set; }

        public HttpOAuthModule(HttpOAuthConfig config)
        {
            Config = config;
        }

     

        public string Name { get; }
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseOAuth(oauthCfg => { oauthCfg.SetPublicSigningKey(Config.PublicSigningKey); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);

            if (Config.IsServerEnabled)
            {
                services.AddScoped(typeof(IOAuthClientStorage), Config.ServerConfig.ClientStorage);
                services.AddScoped(typeof(IOAuthRefreshTokenStorage), Config.ServerConfig.RefreshTokenStorage);
                services.AddScoped(typeof(IOAuthUserProvider), Config.ServerConfig.UserProvider);
                services.AddScoped<IOAuthClientService, OAuthClientService>();
                services.AddScoped<IOAuthRefreshTokenService, OAuthRefreshTokenService>();
                services.AddScoped<IOAuthAccessTokenService, OAuthAccessTokenService>();
            }
        }
    }
}