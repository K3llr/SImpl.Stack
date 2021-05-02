using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simpl.Oauth.Models;

namespace Simpl.Oauth.Api
{
    public interface IOAuthRefreshTokenService
    {
        OAuthRefreshToken FindValidRefreshToken(string clientId, string token);
        int GetRefreshTokenLifetimeSeconds(OAuthClient client);
        OAuthRefreshToken GenerateRefreshToken(HttpRequest request,string userId, OAuthClient client, List<string> scopes);
    }
}