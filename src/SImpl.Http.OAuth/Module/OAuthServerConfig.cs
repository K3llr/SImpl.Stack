using System;
using SImpl.Http.OAuth.Storage;

namespace SImpl.Http.OAuth.Module
{
    public class OAuthServerConfig
    {
        public OAuthServerConfig()
        {
           
        }

        internal Type  ClientStorage { get; set; } = typeof(InMemoryOAuthClientStorage);
        public void RegisterClientStorage<TClientStorage>()
            where TClientStorage : class, IOAuthClientStorage
        {
            ClientStorage = typeof(TClientStorage);
        }

        internal Type RefreshTokenStorage { get; set; } = typeof(InMemoryOAuthRefreshTokenStorage);
        public void RegisterRefreshTokenStorage<TRefreshTokenStorage>()
            where TRefreshTokenStorage : class, IOAuthRefreshTokenStorage
        {
            RefreshTokenStorage = typeof(TRefreshTokenStorage);
        }

        internal Type UserProvider { get; set; }
        public void RegisterUserProvider<TUserProvider>()
            where TUserProvider : class, IOAuthUserProvider
        {
            UserProvider = typeof(TUserProvider);
        }

        internal string PrivateSigningKey { get; private set; }

        /// <summary>
        /// Set a signing key that only your oauth server can know, used to sign JWT tokens
        /// See <see cref="https://novicell.atlassian.net/wiki/spaces/NK/pages/1469121765/Key+generation">Documentation</see>
        /// </summary>
        /// <param name="key"></param>
        public void SetPrivateSigningKey(string key)
        {
            PrivateSigningKey = key;
        }

        /// <summary>
        /// Default access token lifetime in seconds, defaults to 1 hour
        /// </summary>
        internal int DefaultAccessTokenLifetimeSeconds { get; private set; } = 3600;
        public void SetDefaultAccessTokenLifetimeSeconds(int seconds)
        {
            DefaultAccessTokenLifetimeSeconds = seconds;
        }

        /// <summary>
        /// Default refresh token lifetime in seconds, defaults to 7 days
        /// </summary>
        internal int DefaultRefreshTokenLifetimeSeconds { get; private set; } = 604800;
        public void SetDefaultRefreshTokenLifetimeSeconds(int seconds)
        {
            DefaultRefreshTokenLifetimeSeconds = seconds;
        }
    }
}
