using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todos.Database.Models;
using Todos.Options;

namespace Todos.Features.Auth
{
    public class JwtService
    {
        private readonly JwtOptions _jwtOptions;
        public JwtService(IConfiguration configuration)
        {
            _jwtOptions = new JwtOptions(configuration);
        }

        public string GenerateJWTToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var issuedAt = DateTime.Now;
            var issuedAtTicks = issuedAt.Ticks.ToString();

            var expires = issuedAt.AddHours(8);
            var expiresTicks = expires.Ticks.ToString();

            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id + user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim($"{JwtRegisteredClaimNames.Exp}2", expiresTicks),
                new Claim(JwtRegisteredClaimNames.Iat, issuedAtTicks)
            };
            var token = new JwtSecurityToken(issuer, audience, claims: claims, issuedAt, expires: expires, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
