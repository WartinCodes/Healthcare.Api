using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class HemogramaService : IHemogramaService
    {
        private readonly IHemogramaRepository _hemogramaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HemogramaService(IHemogramaRepository hemogramaRepository, IUnitOfWork unitOfWork)
        {
            _hemogramaRepository = hemogramaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Hemograma> Add(Hemograma entity)
        {
            var record = await _unitOfWork.HemogramaRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(Hemograma entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hemograma>> GetAsync()
        {
            return _hemogramaRepository.GetAsync();
        }

        public Task<Hemograma> GetHemogramaByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hemograma entity)
        {
            _unitOfWork.HemogramaRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
