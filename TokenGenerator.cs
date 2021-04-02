using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenGenerator
{
    public class TokenGenerator
    {
        private readonly TokenSettings _tokenSettings;

        public TokenGenerator(TokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public string Generate(IEnumerable<Claim> claims = null)
        {
            claims = claims ?? new Claim[] { };

            var signingKey = GenerateSigningKey(_tokenSettings.SigningKey);

            var token = new JwtSecurityToken(
              issuer: _tokenSettings.Issuer,
              audience: _tokenSettings.Audience,
              claims: claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddHours(_tokenSettings.ExpiresHours),
              signingCredentials: signingKey);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static SigningCredentials GenerateSigningKey(string secret)
        {
            var bytes = Encoding.ASCII.GetBytes(secret);
            var securityKey = new SymmetricSecurityKey(bytes);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            return signingCredentials;
        }
    }
}
