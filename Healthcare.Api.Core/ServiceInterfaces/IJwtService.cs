using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, IList<string> roles);

        Task<bool> ValidatePatientToken(User user);
    }
}
