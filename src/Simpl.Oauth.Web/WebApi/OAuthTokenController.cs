using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simpl.Oauth.Api;
using Simpl.Oauth.Constants;
using Simpl.Oauth.Models;
using Simpl.Oauth.WebApi.RequestModels;
using Simpl.Oauth.WebApi.ResponseModels;

namespace Simpl.Oauth.WebApi
{
    [ApiController]
    public class OAuthTokenController : ControllerBase
    {
        private readonly IOAuthClientService _oAuthClientService;
        private readonly IOAuthRefreshTokenService _oAuthRefreshTokenService;
        private readonly IOAuthAccessTokenService _oAuthAccessTokenService;
        private readonly IOAuthUserProvider _oAuthUserProvider;
        private readonly IOAuthRefreshTokenStorage _oAuthRefreshTokenStorage;

        public OAuthTokenController(
            IOAuthClientService oAuthClientService,
            IOAuthRefreshTokenService oAuthRefreshTokenService,
            IOAuthAccessTokenService oAuthAccessTokenService,
            IOAuthUserProvider oAuthUserProvider,
            IOAuthRefreshTokenStorage oAuthRefreshTokenStorage)
        {
            _oAuthClientService = oAuthClientService;
            _oAuthRefreshTokenService = oAuthRefreshTokenService;
            _oAuthAccessTokenService = oAuthAccessTokenService;
            _oAuthUserProvider = oAuthUserProvider;
            _oAuthRefreshTokenStorage = oAuthRefreshTokenStorage;
        }

        [Route("oauth/token")]
        [HttpPost]
        public HttpResponseMessage Token([FromBody]OAuthTokenRequest tokenRequest)
        {
            HttpContext.Response.Headers.Add("Cache-Control", "no-store");
            HttpContext.Response.Headers.Add("Pragma", "no-store");

            // Fetch client and validate
            var client = _oAuthClientService.FindValidClient(
                tokenRequest.ClientId,
                tokenRequest.ClientSecret,
                tokenRequest.GrantType);
            if (client == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidClient)))
                };
                
            }

            switch (tokenRequest.GrantType)
            {
                case OAuthGrantTypes.Password:
                    return ProcessPasswordGrant(client, tokenRequest);
                case OAuthGrantTypes.RefreshToken:
                    return ProcessRefreshTokenGrant(client, tokenRequest);
            }
            var answer = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject( new OAuthTokenErrorResponse(OAuthTokenErrors.InvalidGrant)))
            };
            return answer;
           
        }

        private HttpResponseMessage ProcessPasswordGrant(OAuthClient client, OAuthTokenRequest tokenRequest)
        {
            var scopes = (tokenRequest.Scopes ?? "")
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (!_oAuthUserProvider.ValidateUser(tokenRequest.Username, tokenRequest.Password, scopes))
            {
                // User is invalid
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            // Get claims
            var claims = _oAuthUserProvider.GetClaimsByUsername(tokenRequest.Username);
            var userId = _oAuthUserProvider.GetUserId(tokenRequest.Username);

            // TODO: Move ClaimsIdentity generation to service?
            var claimsIdentity = new ClaimsIdentity("OAuth");
            claimsIdentity.AddClaims(claims);

            var accessTokenExpiresIn = _oAuthAccessTokenService.GetAccessTokenLifetimeSeconds(client);
            var accessToken = _oAuthAccessTokenService.GenerateAccessToken(claimsIdentity, client);
            var refreshToken = _oAuthRefreshTokenService.GenerateRefreshToken(Request,userId, client, scopes);

            // Persist refresh token
            _oAuthRefreshTokenStorage.Save(refreshToken);
            var answer = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new OAuthTokenResponse
                {
                    TokenType = OAuthTokenTypes.Bearer,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    ExpiresIn = accessTokenExpiresIn,
                    Scope = string.Join(" ", scopes),
                }))
            };
            return answer;
        }

        private HttpResponseMessage ProcessRefreshTokenGrant(OAuthClient client, OAuthTokenRequest tokenRequest)
        {
            // Validate refresh token
            var refreshToken =
                _oAuthRefreshTokenService.FindValidRefreshToken(tokenRequest.ClientId, tokenRequest.RefreshToken);
            if (refreshToken == null)
            {
                // Refresh token is invalid
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            
            // Get claims
            var claims = _oAuthUserProvider.GetClaimsByUserId(refreshToken.UserId);

            // TODO: Move ClaimsIdentity generation to service?
            var claimsIdentity = new ClaimsIdentity("OAuth");
            claimsIdentity.AddClaims(claims);

            var accessTokenExpiresIn = _oAuthAccessTokenService.GetAccessTokenLifetimeSeconds(client);
            var accessToken = _oAuthAccessTokenService.GenerateAccessToken(claimsIdentity, client);
            var newRefreshToken = _oAuthRefreshTokenService.GenerateRefreshToken(Request,
                refreshToken.UserId, 
                client, 
                tokenRequest.Scopes
                ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList());
            
            // Persist refresh new token and delete old
            _oAuthRefreshTokenStorage.Delete(refreshToken.Token);
            _oAuthRefreshTokenStorage.Save(newRefreshToken);
            var answer = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new OAuthTokenResponse
                {
                    TokenType = OAuthTokenTypes.Bearer,
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken.Token,
                    ExpiresIn = accessTokenExpiresIn,
                }))
            };
            return answer;
        }
    }
}
