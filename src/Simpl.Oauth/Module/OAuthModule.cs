using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace Simpl.Oauth.Module
{
    public class OAuthModule : IServicesCollectionConfigureModule
    {
        public OAuthConfig Config { get; }

        public OAuthModule(OAuthConfig config)
        {
            Config = config;
        }

        public string Name => nameof(OAuthModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            services.AddScoped(typeof(ITokenService), Config.TokenServiceType.ImplType);
        }
    }
}