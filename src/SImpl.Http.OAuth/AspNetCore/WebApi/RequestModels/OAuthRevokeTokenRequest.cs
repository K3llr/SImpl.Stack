using System.Runtime.Serialization;

namespace SImpl.Http.OAuth.AspNetCore.WebApi.RequestModels
{
    [DataContract]
    public class OAuthRevokeTokenRequest
    {
        [DataMember(Name = "token_type_hint")]
        public string TokenTypeHint { get; set; } = "refresh_token";

        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}