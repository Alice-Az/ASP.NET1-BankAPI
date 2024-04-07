using Bank.Core.Interfaces;
using Bank.Domain.DTO.Options;
using Bank.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string? GetToken(User user, IList<string> roles)
        {
            if (user != null)
            {
                SecurityOptions security = _config.GetSection("Security").Get<SecurityOptions>() ?? new();
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
                //roles.Select(r => claims.Add(new Claim(ClaimTypes.Role, r.ToString())));

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.Key));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: security.Issuer,
                    audience: security.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),

                    signingCredentials: signInCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return tokenString;
            }
            return null;
        }
    }
}
