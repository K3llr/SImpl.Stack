using System;
using SImpl.Host.Builders;

namespace Simpl.Oauth.Configuration
{
    public class OAuthWebConfig
    {
        internal bool IsServerEnabled { get; private set; }
        public ISImplHostBuilder NovicellAppBuilder { get; }
        internal OAuthServerConfig ServerConfig { get; private set; }

        public OAuthWebConfig(ISImplHostBuilder appBuilder)
        {
            NovicellAppBuilder = appBuilder;
            ServerConfig = new OAuthServerConfig();
          
        }

        public void UseServer(Action<OAuthServerConfig> configure = null)
        {
            configure?.Invoke(ServerConfig);
            IsServerEnabled = true;
        }

        internal string PublicSigningKey { get; private set; }

        /// <summary>
        /// Set a public signing key that can verify your tokens.
        /// See <see cref="https://novicell.atlassian.net/wiki/spaces/NK/pages/1469121765/Key+generation">Documentation</see>
        /// </summary>
        /// <param name="key"></param>
        public void SetPublicSigningKey(string key)
        {
            PublicSigningKey = key;
        }
    }
}
