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
    }
}
