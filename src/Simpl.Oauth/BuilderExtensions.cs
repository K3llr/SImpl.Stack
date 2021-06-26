using System;
using SImpl.Host.Builders;
using Simpl.Oauth.Module;

namespace Simpl.Oauth
{
    public static class AppBuilderExtensions
    {
        public static void UseOAuth(this ISImplHostBuilder simplHostBuilder, Action<OAuthConfig> configure = null)
        {
            var existingModule = simplHostBuilder.GetConfiguredModule<OAuthModule>();

            var config = existingModule != null
                ? existingModule.Config
                : AttachOAuth(simplHostBuilder);

            // Apply user configuration to module
            configure?.Invoke(config);
        }

        private static OAuthConfig AttachOAuth(ISImplHostBuilder simplHostBuilder)
        {
            var config = new OAuthConfig();
            var oauthModule = new OAuthModule(config);

            simplHostBuilder.AttachNewOrGetConfiguredModule(() => oauthModule);

            return config;
        }
    }
}