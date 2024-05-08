using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Core.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByResetTokenAsync(this UserManager<User> um, string resetToken)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.ResetPasswordToken == resetToken);
        }

        public static async Task<User> GetUserById(this UserManager<User> um, int userId)
        {
            return await um?.Users?
                .Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
