using Bookworm_API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Bookworm_API.Services
{
    public class Authorization
    {
        public static string GenerateJWT(tblLeitor leitor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[] {
                new Claim("UserId", $"{leitor.IDLeitor}")
            };

            var token = tokenHandler.CreateJwtSecurityToken(
                subject: new ClaimsIdentity(claims),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(WebApiApplication.JWTSecret)), "HS256")
            );
            return tokenHandler.WriteToken(token);
        }
    }
}