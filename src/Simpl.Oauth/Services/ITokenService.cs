using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Simpl.Oauth.Services
{
    public interface ITokenService
    {
        JwtSecurityToken ReadToken(string token);
        bool Validate(string token);
        IEnumerable<Claim> GetClaims(string token);
    }
}