using System;
using SImpl.Host.Builders;

namespace Simpl.Oauth.Configuration
{
    public static class WebAppBuilderExtensions
    {
        public static void UseWebOAuth(this ISImplHostBuilder webAppBuilder, Action<OAuthWebConfig> configure = null)
        {
            var existingModule = webAppBuilder.GetConfiguredModule<OAuthWebModule>();

            var config = existingModule != null
                ? existingModule.WebConfig
                : Attach(webAppBuilder);

            // Apply user configuration to module
            configure?.Invoke(config);
        }

        private static OAuthWebConfig Attach(ISImplHostBuilder simplHostBuilder)
        {
            var config = new OAuthWebConfig(simplHostBuilder);
            var oauthModule = new OAuthWebModule(config);

            simplHostBuilder.AttachNewOrGetConfiguredModule(()=>oauthModule);

            return config;
        }
    }
}
