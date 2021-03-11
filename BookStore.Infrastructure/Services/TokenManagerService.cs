using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.Shared.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStore.Infrastructure.Services
{
    public class TokenManagerService :  ITokenManagerService
    {
        private readonly IConfigurations configs;
        public TokenManagerService(IConfigurations configs) 
        {
            this.configs = configs;
        }
        public string CreateToken(TokenData input)
        {
            var claims = new[]
            {
                new Claim("Id", input.Id.ToString()),
                new Claim("Guid", input.Guid),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configs.JwtScrete));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: input.ExpirationDate,
                signingCredentials: credential
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }

        public Either<NotificationBase, TokenData> VerifyToken(string token)
        {
            try
            {
                var jwtKey = Encoding.ASCII.GetBytes(configs.JwtScrete);
                var jwtHandler = new JwtSecurityTokenHandler();
                var validators = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                var claims = jwtHandler
                    .ValidateToken(token, validators, out var tokenSecure)
                    .Claims.ToArray();

                var Id = Convert.ToInt32(claims[0].Value);
                var Guid = claims[1].Value;

                var tokenData =  new TokenData() 
                { 
                    Id = Id,
                    Guid = Guid
                };
                return new Right<NotificationBase, TokenData>(tokenData);
            }
            catch
            {
                return new Left<NotificationBase, TokenData> (new NotificationBase(""));
            }
        }
    }
}
