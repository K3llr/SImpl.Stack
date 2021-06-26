using System.Runtime.Serialization;

namespace SImpl.Http.OAuth.WebApi.ResponseModels
{
    [DataContract]
    public class OAuthTokenErrorResponse
    {
        public OAuthTokenErrorResponse()
        {
            
        }

        public OAuthTokenErrorResponse(string error)
        {
            Error = error;
        }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string ErrorDescription { get; set; }
    }
}
