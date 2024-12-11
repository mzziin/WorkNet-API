using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkNet.BLL.DTOs;

namespace WorkNet.BLL.SecurityServices
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserDTO user, int? candidateId = null, int? employerId = null)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!),
                new Claim("UserId", user.UserId.ToString()) // Add UserId as a unique identifier
            };

            // Add either CandidateId or EmployerId based on role
            if (candidateId.HasValue)
            {
                authClaims.Add(new Claim("CandidateId", candidateId.Value.ToString()));
            }
            if (employerId.HasValue)
            {
                authClaims.Add(new Claim("EmployerId", employerId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}