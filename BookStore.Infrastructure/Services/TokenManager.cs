using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Infrastructure.Services
{
    public class TokenManager :  ITokenManager
    {
        public string CreateToken(TokenData input)
        {
            var claims = new[]
            {
                new Claim("Id", input.Id.ToString()),
                new Claim("Guid", input.Guid),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qw456q4w56ewq456eqwe456wqe456654qweqw6e45"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credential
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
