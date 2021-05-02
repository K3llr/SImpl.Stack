using Simpl.Oauth.Models;

namespace Simpl.Oauth.Api
{
    public interface IOAuthRefreshTokenStorage
    {
        void Save(OAuthRefreshToken refreshToken);
        void Delete(string token);
        OAuthRefreshToken FetchByToken(string token);
    }
}