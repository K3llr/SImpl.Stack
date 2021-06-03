using Simpl.Oauth.Models;

namespace Simpl.Oauth.Api
{
    public interface IOAuthClientService
    {
        OAuthClient FindValidClient(string clientId, string clientSecret, string grantType);
    }
}