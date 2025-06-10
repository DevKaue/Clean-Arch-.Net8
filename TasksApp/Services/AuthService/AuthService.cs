using Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthService
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        public string GenerateJWT(string email, string username)
        {
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var key = _configuration["JWT:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Email",email),
                new("Username",username),
                new("EmailIdentifier", email.Split("@").ToString()!), //Idenficicador é o que vem antes do @
                new("CurrentTime", DateTime.Now.ToString())            
            };

            _ = int.TryParse(_configuration["JWT:TokenExpirationTimeHours"], out int tokenExpirationTimeHours);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: DateTime.Now.AddHours(tokenExpirationTimeHours), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }   
    
        public string GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[128];
            using var randonNumberGenerator = RandomNumberGenerator.Create();

            randonNumberGenerator.GetBytes(secureRandomBytes);

            return Convert.ToBase64String(secureRandomBytes);
        }

        public string HashingPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();

            for (int i = 0; i < bytes.Length; i++)
            {
                    builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
