using System.Collections.Concurrent;
using System.Collections.Generic;
using Simpl.Oauth.Api;
using Simpl.Oauth.Models;

namespace Simpl.Oauth.Storage.InMemory
{
    public class InMemoryOAuthRefreshTokenStorage : IOAuthRefreshTokenStorage
    {
        protected static readonly IDictionary<string, OAuthRefreshToken> Storage = new ConcurrentDictionary<string, OAuthRefreshToken>();

        public void Save(OAuthRefreshToken refreshToken)
        {
            if (Storage.ContainsKey(refreshToken.Token))
            {
                Storage.Remove(refreshToken.Token);
            }
            Storage.Add(refreshToken.Token, refreshToken);
        }

        public void Delete(string token)
        {
            if (Storage.ContainsKey(token))
            {
                Storage.Remove(token);
            }
        }

        public OAuthRefreshToken FetchByToken(string token)
        {
            return Storage.ContainsKey(token)
                ? Storage[token]
                : null;
        }
    }
}
