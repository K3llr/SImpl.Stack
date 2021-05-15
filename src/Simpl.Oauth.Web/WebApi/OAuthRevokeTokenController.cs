using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Simpl.Oauth.Api;
using Simpl.Oauth.Constants;
using Simpl.Oauth.WebApi.RequestModels;
using Simpl.Oauth.WebApi.ResponseModels;

namespace Simpl.Oauth.WebApi
{
    [ApiController]
    [Route("TaggingService/[controller]")]
    public class OAuthRevokeTokenController : ControllerBase
    {
        private readonly IOAuthRefreshTokenStorage _oAuthRefreshTokenStorage;
        private readonly IOAuthClientStorage _oAuthClientStorage;

        public OAuthRevokeTokenController(
            IOAuthRefreshTokenStorage oAuthRefreshTokenStorage,
            IOAuthClientStorage oAuthClientStorage)
        {
            _oAuthRefreshTokenStorage = oAuthRefreshTokenStorage;
            _oAuthClientStorage = oAuthClientStorage;
        }

        [Route("oauth/revoke")]
        [HttpPost]
        public HttpResponseMessage Revoke([FromBody] OAuthRevokeTokenRequest revokeRequest)
        {
            if (string.IsNullOrWhiteSpace(revokeRequest.ClientId)
                || string.IsNullOrWhiteSpace(revokeRequest.ClientSecret)
                || string.IsNullOrWhiteSpace(revokeRequest.Token))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(
                        new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidRequest)
                        {
                            ErrorDescription = "client_id, client_secret and token is required."
                        }))
                };
            }

            var client = _oAuthClientStorage.Fetch(revokeRequest.ClientId);

            if (client == null 
                || client.ClientSecret != revokeRequest.ClientSecret)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(
                        new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidClient)))
                };
            }

            try
            {
                if (revokeRequest.TokenTypeHint != OAuthTokenTypes.RefreshToken)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(
                            new OAuthTokenErrorResponse("unsupported_token_type")))
                    };
                }

                _oAuthRefreshTokenStorage.Delete(revokeRequest.Token);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(
                        new OAuthTokenErrorResponse()
                        {
                            ErrorDescription = e.Message
                        }))
                };
            }
        }

      
    }
}