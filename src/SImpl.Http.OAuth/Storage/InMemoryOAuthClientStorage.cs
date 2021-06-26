using System.Collections.Concurrent;
using System.Collections.Generic;
using SImpl.Http.OAuth.Models;

namespace SImpl.Http.OAuth.Storage
{
    public class InMemoryOAuthClientStorage : IOAuthClientStorage
    {
        protected static readonly IDictionary<string, OAuthClient> Storage = new ConcurrentDictionary<string, OAuthClient>();

        public void Save(OAuthClient client)
        {
            if (Storage.ContainsKey(client.ClientId))
            {
                Storage.Remove(client.ClientId);
            }
            Storage.Add(client.ClientId, client);
        }

        public void Delete(string clientId)
        {
            if (Storage.ContainsKey(clientId))
            {
                Storage.Remove(clientId);
            }
        }

        public OAuthClient Fetch(string clientId)
        {
            return Storage.ContainsKey(clientId)
                ? Storage[clientId]
                : null;
        }
    }
}
