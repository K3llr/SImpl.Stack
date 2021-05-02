using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using Simpl.Oauth.Configuration;
using Simpl.Oauth.Services;

namespace Simpl.Oauth
{
    public class OAuthModule : IServicesCollectionConfigureModule
    {
        public OAuthConfig Config { get; }

        public OAuthModule(OAuthConfig config)
        {
            Config = config;
        }


        public string Name { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            services.AddScoped(typeof(ITokenService), Config.TokenService);

        }
    }
}