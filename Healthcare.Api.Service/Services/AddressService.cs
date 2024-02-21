using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IAddressRepository addressRepository, IUnitOfWork unitOfWork)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Add(Address entity)
        {
            var record = await _unitOfWork.AddressRepository.AddAsync(entity);
            _unitOfWork.Save();
            return record;
        }

        public void Edit(Address entity)
        {
            _unitOfWork.AddressRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Address> GetAsQueryable()
        {
            return _addressRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Address>> GetAsync()
        {
            return _addressRepository.GetAsync();
        }

        public void Remove(Address entity)
        {
            _unitOfWork.AddressRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
