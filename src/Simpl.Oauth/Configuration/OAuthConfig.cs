using System;
using SImpl.Host.Builders;
using Simpl.Oauth.Services;

namespace Simpl.Oauth.Configuration
{
    public class OAuthConfig
    {
        public ISImplHostBuilder NovicellAppBuilder { get; }

        public OAuthConfig(ISImplHostBuilder novicellAppBuilder)
        {
            NovicellAppBuilder = novicellAppBuilder;
          
        }

        internal Type TokenService { get; set; } = typeof(JwtTokenService);

        public void RegisterTokenService<TTokenService>()
            where TTokenService : class, ITokenService
        {
            TokenService = typeof(TTokenService);
        }

        internal string PublicSigningKey { get; set; }
        /// <summary>
        /// Set a public signing key. Should be generated with ssh-keygen
        /// </summary>
        /// <param name="publicKey">G</param>
        public void SetPublicSigningKey(string publicKey)
        {
            PublicSigningKey = publicKey;
        }
    }
}