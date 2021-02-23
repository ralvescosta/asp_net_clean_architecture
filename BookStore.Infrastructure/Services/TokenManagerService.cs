using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

        public TokenData VerifyToken(string Token)
        {
            return new TokenData 
            { 
                Id = 1,
                Guid = Guid.NewGuid().ToString()
            };
        }
    }
}
