using Simpl.Oauth.Api;
using Simpl.Oauth.Constants;
using Simpl.Oauth.Models;

namespace Simpl.Oauth.Services
{
    public class OAuthClientService : IOAuthClientService
    {
        private readonly IOAuthClientStorage _oAuthClientStorage;

        public OAuthClientService(IOAuthClientStorage oAuthClientStorage)
        {
            _oAuthClientStorage = oAuthClientStorage;
        }

        public OAuthClient FindValidClient(string clientId, string clientSecret, string grantType)
        {
            var client = _oAuthClientStorage.Fetch(clientId);
            if (client == null) return null;

            // TODO: Change AllowedGrantTypes for allowed flows? (PasswordGrant etc)

            // Make sure client is valid
            if (!client.IsActive // Client inactive
                || client.ClientSecret != clientSecret // Invalid secret
                || grantType != OAuthGrantTypes.RefreshToken && !client.AllowedGrantTypes.Contains(grantType)) // Invalid grant type
            {
                return null;
            }

            // Client is valid
            return client;
        }
    }
}
