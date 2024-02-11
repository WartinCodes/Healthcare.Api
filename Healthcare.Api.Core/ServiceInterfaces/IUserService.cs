using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IUserService
{
        Task<User> FindUserByEmailOrDni(string email, string dni);
        Task<Boolean> ValidateUserCredentials(string user, string password);
    }
}
