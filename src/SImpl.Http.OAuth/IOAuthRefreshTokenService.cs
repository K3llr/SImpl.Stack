using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth
{
    public interface IOAuthRefreshTokenService
    {
        OAuthRefreshToken FindValidRefreshToken(string clientId, string token);
        int GetRefreshTokenLifetimeSeconds(OAuthClient client);
        OAuthRefreshToken GenerateRefreshToken(HttpRequest request,string userId, OAuthClient client, List<string> scopes);
    }
}