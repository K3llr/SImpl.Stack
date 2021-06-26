using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth.Api
{
    public interface IOAuthClientService
    {
        OAuthClient FindValidClient(string clientId, string clientSecret, string grantType);
    }
}