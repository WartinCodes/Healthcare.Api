using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IAuthService authService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> FindUserByEmailOrDni(string email, string dni)
        {
            return await _unitOfWork.UserRepository.FindUserByEmailOrDni(email, dni);
        }

        public async Task<User> AddAsync(User entity)
        {
            var record = await _unitOfWork.UserRepository.AddAsync(entity);
            _unitOfWork.Save();
            return record;
        }

        public void Edit(User entity)
        {
            _unitOfWork.UserRepository.Edit(entity);
            _unitOfWork.Save();
        }

        //public IQueryable<User> GetAsQueryable()
        //{
        //    return new User();
        //}

        public async Task<IEnumerable<User>> GetAsync()
        {
            return new List<User>();
        }

        public void Remove(User entity)
        {
            _unitOfWork.UserRepository.Remove(entity);
            _unitOfWork.Save();
        }

        public async Task<Boolean> ValidateUserCredentials(string user, string password)
        {
            var passwordHash = _authService.EncryptPassword(password);
            return await _unitOfWork.UserRepository.ValidateUserCredentials(user, passwordHash);
        }
    }
}
