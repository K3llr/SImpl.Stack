using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using SImpl.OAuth.Constants;
using SImpl.OAuth.Module;

namespace SImpl.OAuth.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly OAuthConfig _config;
        private readonly IConfiguration _configuration;

        public JwtTokenService(OAuthConfig config, IConfiguration configuration)
        {
            _config = config;
            _configuration = configuration;
        }

        public JwtSecurityToken ReadToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                return tokenHandler.ReadJwtToken(token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Validate(string token)
        {
            var handler = new JsonWebTokenHandler();

            RSAParameters rsaParams;
            var key = _config.PublicSigningKey;
            
            // Backwards compatability: If no key has been configured, read from configuration
            if (string.IsNullOrWhiteSpace(key))
            {
                _config.ReadPublicSigningKeyFromConfiguration(_configuration);
                key = _config.PublicSigningKey;
            }
            
            // If still no key, throw exception
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ApplicationException("No public signing key has been configured.");
            }

            using (var tr = new StringReader(key))
            {
                var pemReader = new PemReader(tr);
                var publicKeyParams = pemReader.ReadObject() as RsaKeyParameters;
                if (publicKeyParams == null)
                {
                    throw new Exception("Could not read RSA public key");
                }
                rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParams);
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
            }

            TokenValidationResult result = handler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = OAuthIssuers.Standard,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new RsaSecurityKey(rsaParams)
                });

            return result != null && result.IsValid;
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            if (securityToken == null)
            {
                return new List<Claim>();
            }

            return securityToken.Claims;
        }
    }
}