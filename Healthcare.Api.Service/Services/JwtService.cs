using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Healthcare.Api.Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string GenerateToken(User user, IList<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(nameof(user.Id), user.Id.ToString()),
                new Claim(nameof(user.Email), user.Email),
                new Claim(nameof(user.FirstName), user.FirstName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidatePatientToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var currentUserId = userClaims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (int.TryParse(currentUserId, out int parsedUserId) && parsedUserId == user.Id)
            {
                return true;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(RoleEnum.Medico) || userRoles.Contains(RoleEnum.Secretaria))
            {
                return true;
            }

            return false;
        }

    }
}
