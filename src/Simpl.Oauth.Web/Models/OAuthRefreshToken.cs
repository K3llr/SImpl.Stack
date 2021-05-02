using System;

namespace Simpl.Oauth.Models
{
    public class OAuthRefreshToken
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string ClientId { get; set; }

        public DateTime IssuedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public string Description { get; set; }
    }
}