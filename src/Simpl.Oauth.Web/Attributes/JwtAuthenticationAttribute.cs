using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Simpl.DependencyInjection;
using Simpl.Oauth.ActionResults;
using Simpl.Oauth.Constants;
using Simpl.Oauth.Services;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;

namespace Simpl.Oauth.Attributes
{
    public class JwtAuthenticationAttribute : Attribute, IAuthorizationFilter,IAsyncAuthorizationFilter , IContainerAware
    {
        private ITokenService _tokenService;

        public ITokenService TokenService
        {
            get => _tokenService ?? this.Resolve<ITokenService>();
            private set => _tokenService = value;
        }

        public bool AllowMultiple { get; } = true;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // Make sure we have the proper header
            var authorization = context.Request.Headers.Authorization;
            if (authorization == null
                || authorization.Scheme != "Bearer")
            {
                // No header or different scheme
                return Task.FromResult(0);
            }

            if (string.IsNullOrWhiteSpace(authorization.Parameter))
            {
                // No token provided
                context.ErrorResult = new AuthenticationFailureResult(context.Request, "Missing token");
                return Task.FromResult(0);
            }

            // Validate JWT token signature
            var token = authorization.Parameter;
            if (!TokenService.Validate(token))
            {
                // Invalid token signature
                context.ErrorResult = new AuthenticationFailureResult(context.Request, "Invalid token");
                return Task.FromResult(0);
            }

            // Read JWT token contents
            var securityToken = TokenService.ReadToken(token);
            if (securityToken == null)
            {
                // Invalid token format
                context.ErrorResult = new AuthenticationFailureResult(context.Request, "Invalid token");
                return Task.FromResult(0);
            }

            // Read user id
            var userId = securityToken.Claims.SingleOrDefault(x => x.Type == OAuthClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                // Missing user id claim
                context.ErrorResult = new AuthenticationFailureResult(context.Request, $"Missing claim '{OAuthClaimTypes.NameIdentifier}'");
                return Task.FromResult(0);
            }

            // Extract claims
            var claims = securityToken.Claims.ToList();

            // Add Identity claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            // Token and claim payload valid, generate principal
            var identity = new ClaimsIdentity(claims, "ExternalBearer");
            context.Principal = new ClaimsPrincipal(identity);

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}