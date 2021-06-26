using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth.Api
{
    public interface IOAuthClientStorage
    {
        void Save(OAuthClient client);
        void Delete(string clientId);
        OAuthClient Fetch(string clientId);
    }
}
