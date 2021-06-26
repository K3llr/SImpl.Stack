using System.Security.Claims;
using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth
{
    public interface IOAuthAccessTokenService
    {
        int GetAccessTokenLifetimeSeconds(OAuthClient client);
        string GenerateAccessToken(ClaimsIdentity identity, OAuthClient client);
    }
}