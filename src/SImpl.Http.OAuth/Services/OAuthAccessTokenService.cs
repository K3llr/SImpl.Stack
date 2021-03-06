﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using SImpl.Http.OAuth.Models;
using SImpl.Http.OAuth.Module;
using SImpl.OAuth.Constants;

namespace SImpl.Http.OAuth.Services
{
    public class OAuthAccessTokenService : IOAuthAccessTokenService
    {
        private readonly HttpOAuthConfig _config;

        public OAuthAccessTokenService(HttpOAuthConfig config)
        {
            _config = config;
        }

        public int GetAccessTokenLifetimeSeconds(OAuthClient client)
        {
            return client.AccessTokenLifetimeSeconds ?? _config.ServerConfig.DefaultAccessTokenLifetimeSeconds;
        }

        public string GenerateAccessToken(ClaimsIdentity identity, OAuthClient client)
        {
            var expirySeconds = GetAccessTokenLifetimeSeconds(client);

            RSAParameters rsaParams;

            using (var tr = new StringReader(_config.ServerConfig.PrivateSigningKey))
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
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}
