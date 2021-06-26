using System;
using SImpl.Host.Builders;
using SImpl.Http.OAuth.Module;

namespace SImpl.Http.OAuth
{
    public static class WebAppBuilderExtensions
    {
        public static void UseWebOAuth(this ISImplHostBuilder webAppBuilder, Action<HttpOAuthConfig> configure = null)
        {
            var existingModule = webAppBuilder.GetConfiguredModule<HttpOAuthModule>();

            var config = existingModule != null
                ? existingModule.Config
                : Attach(webAppBuilder);

            // Apply user configuration to module
            configure?.Invoke(config);
        }

        private static HttpOAuthConfig Attach(ISImplHostBuilder simplHostBuilder)
        {
            var config = new HttpOAuthConfig(simplHostBuilder);
            var oauthModule = new HttpOAuthModule(config);

            simplHostBuilder.AttachNewOrGetConfiguredModule(()=>oauthModule);

            return config;
        }
    }
}
