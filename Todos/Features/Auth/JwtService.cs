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
            var expires = DateTime.Now.AddHours(4);
            var expiresTicks = expires.Ticks.ToString();
            var issuedAt = DateTime.Now;
            var issuedAtTicks = issuedAt.Ticks.ToString();
            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sid, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName!));
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id + user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer));
            claims.Add(new Claim($"{JwtRegisteredClaimNames.Exp}2", expiresTicks));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, issuedAtTicks));
            var token = new JwtSecurityToken(issuer, audience, claims: claims, issuedAt, expires: expires, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
