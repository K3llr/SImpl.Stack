using System;
using SImpl.Host.Builders;

namespace Simpl.Oauth.Configuration
{
    public static class AppBuilderExtensions
    {
        public static void UseOAuth(this ISImplHostBuilder novicellAppBuilder, Action<OAuthConfig> configure = null)
        {
            var existingModule = novicellAppBuilder.GetConfiguredModule<OAuthModule>();

            var config = existingModule != null
                ? existingModule.Config
                : AttachOAuth(novicellAppBuilder);

            // Apply user configuration to module
            configure?.Invoke(config);
        }

        private static OAuthConfig AttachOAuth(ISImplHostBuilder novicellAppBuilder)
        {
            var config = new OAuthConfig(novicellAppBuilder);
            var oauthModule = new OAuthModule(config);

            novicellAppBuilder.AttachNewOrGetConfiguredModule(()=>oauthModule);

            return config;
        }
    }
}