using Simpl.Oauth.Models;

namespace Simpl.Oauth.Api
{
    public interface IOAuthClientStorage
    {
        void Save(OAuthClient client);
        void Delete(string clientId);
        OAuthClient Fetch(string clientId);
    }
}
