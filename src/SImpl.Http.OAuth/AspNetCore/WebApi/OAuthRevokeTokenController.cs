using System;
using Microsoft.AspNetCore.Mvc;
using SImpl.Http.OAuth.AspNetCore.WebApi.RequestModels;
using SImpl.Http.OAuth.AspNetCore.WebApi.ResponseModels;
using SImpl.OAuth.Constants;

namespace SImpl.Http.OAuth.AspNetCore.WebApi
{
    [ApiController]
    [Route("oauth")]
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

        [HttpPost("revoke")]
        public new IActionResult Revoke([FromBody] OAuthRevokeTokenRequest revokeRequest)
        {
            if (string.IsNullOrWhiteSpace(revokeRequest.ClientId)
                || string.IsNullOrWhiteSpace(revokeRequest.ClientSecret)
                || string.IsNullOrWhiteSpace(revokeRequest.Token))
            {
                return BadRequest(new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidRequest)
                {
                    ErrorDescription = "client_id, client_secret and token is required."
                });
            }

            var client = _oAuthClientStorage.Fetch(revokeRequest.ClientId);

            if (client == null
                || client.ClientSecret != revokeRequest.ClientSecret)
            {
                return BadRequest(new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidClient));
            }

            try
            {
                if (revokeRequest.TokenTypeHint != OAuthTokenTypes.RefreshToken)
                {
                    return BadRequest(new OAuthTokenErrorResponse("unsupported_token_type"));
                }

                _oAuthRefreshTokenStorage.Delete(revokeRequest.Token);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new OAuthTokenErrorResponse()
                {
                    ErrorDescription = e.Message
                });
            }
        }
    }
}