using System;
using Microsoft.Extensions.Configuration;
using SImpl.Common;
using SImpl.OAuth.Services;

namespace SImpl.OAuth.Module
{
    public class OAuthConfig
    {
        internal TypeOf<ITokenService> TokenServiceType { get; private set; }
            = TypeOf.New<ITokenService, JwtTokenService>();

        public OAuthConfig RegisterTokenService<TTokenService>()
            where TTokenService : class, ITokenService
        {
            RegisterTokenService(TypeOf.New<ITokenService, TTokenService>());
            return this;
        }

        public OAuthConfig RegisterTokenService(TypeOf<ITokenService> type)
        {
            TokenServiceType = type;
            return this;
        }
        
        internal string PublicSigningKey { get; set; }
        
        /// <summary>
        /// Set a public signing key. Should be generated with ssh-keygen
        /// </summary>
        /// <param name="publicKey">G</param>
        public OAuthConfig SetPublicSigningKey(string publicKey)
        {
            PublicSigningKey = publicKey;
            return this;
        }

        public OAuthConfig ReadPublicSigningKeyFromConfiguration(IConfiguration configuration)
        {
            var base64PublicKey = configuration[PublicSigningKeyConfigKey];;
            if (!string.IsNullOrWhiteSpace(base64PublicKey))
            {
                var data = Convert.FromBase64String(base64PublicKey);
                PublicSigningKey = System.Text.Encoding.ASCII.GetString(data);
            }
            return this;
        }

        public OAuthConfig ReadPublicSigningKeyFromConfiguration(IConfiguration configuration, string configKey)
        {
            SetPublicSigningKeyConfigKey(configKey);
            return ReadPublicSigningKeyFromConfiguration(configuration);
        }

        public string PublicSigningKeyConfigKey { get; private set; } = "Simpl.OAuth.PublicKey.Base64";

        public OAuthConfig SetPublicSigningKeyConfigKey(string configKey)
        {
            PublicSigningKeyConfigKey = configKey;
            return this;
        }
    }
}