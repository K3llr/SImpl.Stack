using System.Security.Claims;
using Simpl.Oauth.Models;

namespace Simpl.Oauth.Api
{
    public interface IOAuthAccessTokenService
    {
        int GetAccessTokenLifetimeSeconds(OAuthClient client);
        string GenerateAccessToken(ClaimsIdentity identity, OAuthClient client);
    }
}