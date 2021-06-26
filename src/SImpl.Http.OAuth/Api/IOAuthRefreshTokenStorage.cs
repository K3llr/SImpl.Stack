using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth.Api
{
    public interface IOAuthRefreshTokenStorage
    {
        void Save(OAuthRefreshToken refreshToken);
        void Delete(string token);
        OAuthRefreshToken FetchByToken(string token);
    }
}