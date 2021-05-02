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
using Simpl.Oauth.Configuration;
using Simpl.Oauth.Constants;

namespace Simpl.Oauth.Services
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
            if (string.IsNullOrWhiteSpace(key))
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    var base64PublicKey = _configuration["Simpl.OAuth.PublicKey.Base64"];

                    var data = Convert.FromBase64String(base64PublicKey);
                    key = System.Text.Encoding.ASCII.GetString(data);
                }
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