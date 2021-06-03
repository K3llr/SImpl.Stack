using System.Runtime.Serialization;

namespace Simpl.Oauth.WebApi.RequestModels
{
    [DataContract]
    public class OAuthTokenRequest
    {
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }
        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }
        
        [DataMember(Name = "scopes")]
        public string Scopes { get; set; }

        /// <summary>
        /// Username, used in Password Grant
        /// </summary>
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// User password, used in Password Grant
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Refresh token, used in Refresh Token Grant
        /// </summary>
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
