using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Simpl.DependencyInjection;
using Simpl.Oauth.ActionResults;
using Simpl.Oauth.Constants;
using Simpl.Oauth.Services;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;

namespace Simpl.Oauth.Attributes
{
    public class JwtAuthenticationAttribute : Attribute, IAsyncAuthorizationFilter , IContainerAware
    {
        private ITokenService _tokenService;

        public ITokenService TokenService
        {
            get => _tokenService ?? this.Resolve<ITokenService>();
            private set => _tokenService = value;
        }

        public bool AllowMultiple { get; } = true;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
             // Make sure we have the proper header
            var authorization = (string) context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorization)
                || authorization.ToString().StartsWith("Bearer"))
            {
                // No header or different scheme
                context.Result = new EmptyResult();
            }
            string encodedUsernamePassword = authorization.Substring("Basic ".Length).Trim();
            if (string.IsNullOrWhiteSpace(encodedUsernamePassword))
            {
             
            }

            // Validate JWT token signature
            if (!TokenService.Validate(encodedUsernamePassword))
            {
                // Invalid token signature
                context.Result = new AuthenticationFailureResult(new HttpRequestMessage(),
                    "Invalid token");
            }

            // Read JWT token contents
            var securityToken = TokenService.ReadToken(encodedUsernamePassword);
            if (securityToken == null)
            {
                // Invalid token format
                context.Result = new AuthenticationFailureResult(new HttpRequestMessage(),
                    "Invalid token");
            }

            // Read user id
            var userId = securityToken.Claims.SingleOrDefault(x => x.Type == OAuthClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                // Missing user id claim
                context.Result = new AuthenticationFailureResult(new HttpRequestMessage(),
                    $"Missing claim '{OAuthClaimTypes.NameIdentifier}'");
            }

            // Extract claims
            var claims = securityToken.Claims.ToList();

            // Add Identity claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            // Token and claim payload valid, generate principal
            var identity = new ClaimsIdentity(claims, "ExternalBearer");
            context.HttpContext.User = new ClaimsPrincipal(identity);

        }
    }
}