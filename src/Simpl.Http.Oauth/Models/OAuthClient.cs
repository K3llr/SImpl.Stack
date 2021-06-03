using System.Collections.Generic;

namespace Simpl.Oauth.Models
{
    public class OAuthClient
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? AccessTokenLifetimeSeconds{ get; set; }
        public int? RefreshTokenLifetimeSeconds{ get; set; }

        public List<string> AllowedGrantTypes { get; set; }
        public List<string> AllowedOrigins { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
