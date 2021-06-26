using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using SImpl.Http.OAuth.Api;
using SImpl.Http.OAuth.Configuration;
using SImpl.Http.OAuth.Models;
using SImpl.OAuth;
using SImpl.OAuth.Constants;

namespace SImpl.Http.OAuth.Services
{
    public class OAuthRefreshTokenService : IOAuthRefreshTokenService
    {
        private readonly OAuthWebConfig _webConfig;
        private readonly ITokenService _tokenService;
        private readonly IOAuthRefreshTokenStorage _oAuthRefreshTokenStorage;
        private readonly IOAuthUserProvider _oAuthUserProvider;

        public OAuthRefreshTokenService(
            OAuthWebConfig webConfig,
            ITokenService tokenService,
            IOAuthUserProvider oAuthUserProvider,
            IOAuthRefreshTokenStorage oAuthRefreshTokenStorage)
        {
            _webConfig = webConfig;
            _tokenService = tokenService;
            _oAuthUserProvider = oAuthUserProvider;
            _oAuthRefreshTokenStorage = oAuthRefreshTokenStorage;
        }

        public OAuthRefreshToken FindValidRefreshToken(string clientId, string token)
        {
            var isRefreshTokenValid = _tokenService.Validate(token);
            if (!isRefreshTokenValid)
            {
                return null;
            }

            var refreshToken = _oAuthRefreshTokenStorage.FetchByToken(token);
            if (refreshToken == null)
            {
                return null;
            }

            var securityToken = _tokenService.ReadToken(token);
            var userId = securityToken.Claims.FirstOrDefault(x => x.Type.Equals(OAuthClaimTypes.NameIdentifier))?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var rememberToken = _oAuthUserProvider.GetUserRememberToken(userId)  ?? string.Empty;
            var securityTokenRememberToken = securityToken.Claims.FirstOrDefault(x => x.Type.Equals(OAuthClaimTypes.RememberToken))?.Value ?? string.Empty;

            // If user has logged out everwhere, the remember token doesn't match anymore and we log out. 
            if (rememberToken != securityTokenRememberToken)
            {
                _oAuthRefreshTokenStorage.Delete(token);
                return null;
            }

            // Make sure refresh token is valid
            if (refreshToken.ClientId != clientId // Invalid client
                || refreshToken.ExpireAt < DateTime.UtcNow) // Expired
            {
                return null;
            }

            // Refresh token is valid
            return refreshToken;
        }

        public int GetRefreshTokenLifetimeSeconds(OAuthClient client)
        {
            return client.RefreshTokenLifetimeSeconds ?? _webConfig.ServerConfig.DefaultRefreshTokenLifetimeSeconds;
        }

        public OAuthRefreshToken GenerateRefreshToken(HttpRequest request,string userId, OAuthClient client, List<string> scopes)
        {
            var expirySeconds = GetRefreshTokenLifetimeSeconds(client);

            RSAParameters rsaParams;

            using (var tr = new StringReader(_webConfig.ServerConfig.PrivateSigningKey))
            {
                var pemReader = new PemReader(tr);
                var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
                if (keyPair == null)
                {
                    throw new Exception("Could not read RSA private key");
                }
                var privateRsaParams = keyPair.Private as RsaPrivateCrtKeyParameters;
                rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);
            }

            SecurityKey key = new RsaSecurityKey(rsaParams);
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(OAuthClaimTypes.RememberToken, _oAuthUserProvider.GetUserRememberToken(userId) ?? string.Empty));
            identity.AddClaim(new Claim(OAuthClaimTypes.NameIdentifier, userId));
            identity.AddClaim(new Claim(OAuthClaimTypes.Scopes, string.Join(" ", scopes ?? new List<string>())));
            identity.AddClaim(new Claim(OAuthClaimTypes.TokenIdentifier, Guid.NewGuid().ToString()));

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = OAuthIssuers.Standard,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddSeconds(expirySeconds),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(securityToken);

            request.Headers.TryGetValue("User-Agent", out var userAgent);

            return new OAuthRefreshToken
            {
                ClientId = client.ClientId,
                IssuedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddSeconds(GetRefreshTokenLifetimeSeconds(client)),
                UserId = userId,
                Token = refreshToken,
                Description = userAgent
            };
        }
    }
}
