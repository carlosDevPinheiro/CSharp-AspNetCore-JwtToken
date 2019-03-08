using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SecurityToken.ProviderJWT
{   
    public class JwtBearerBuilder
    {
        public static TokenJWT Create(ETypeUser tipo)
        {
            var token = new TokenJWTBuilder()
               .AddSecurityKey(JWTSecurityKey.Create(JwtTokenOptions.Values.Secret_Key))
               .AddSubject(JwtTokenOptions.Values.Subject)
               .AddIssuer(JwtTokenOptions.Values.Issuer)
               .AddAudience(JwtTokenOptions.Values.Audience)
               .AddClaim(tipo.ToString(), "1")
               .AddExpiry(5)
               .Builder();

            return token;


        }

      
    }

    internal class TokenJWTBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 5;


        internal TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        internal TokenJWTBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        internal TokenJWTBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        internal TokenJWTBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        internal TokenJWTBuilder AddClaim(Claim claim)
        {

            this.claims.Add(claim.Type, claim.Value);
            return this;
        }

        internal TokenJWTBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        internal TokenJWTBuilder AddClaims(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }

        internal TokenJWTBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        internal TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
             .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256));

            return new TokenJWT(token);
        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");

        }
    }


}
