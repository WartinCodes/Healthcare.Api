using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Healthcare.Api.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptPassword(string password)
        {
            string seed = _configuration["AppSettings:EncryptionSeed"];
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + seed));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
