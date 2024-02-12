using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> AddAsync(User entity);
        Task<User> GetUserByIdAsync(int id);
        Task<User> FindUserByEmailOrDni(string email, string dni);
        void Remove(User entity);
        void Edit(User entity);
    }
}