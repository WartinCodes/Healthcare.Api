using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class PatientHistoryService : IPatientHistoryService
    {
        private readonly IPatientHistoryRepository _patientHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientHistoryService(IPatientHistoryRepository patientHistoryRepository, IUnitOfWork unitOfWork)
        {
            _patientHistoryRepository = patientHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientHistory> Add(PatientHistory entity)
        {
            var record = await _unitOfWork.PatientHistoryRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(PatientHistory entity)
        {
            _unitOfWork.PatientHistoryRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<PatientHistory> GetAsQueryable()
        {
            return _patientHistoryRepository.GetAsQueryable();
        }

        public async Task<IEnumerable<PatientHistory>> GetAsync()
        {
            return await _patientHistoryRepository.GetAsync();
        }

        public async Task<IEnumerable<PatientHistory>> GetPatientHistoryByUserIdAsync(int userId)
        {
            return await _patientHistoryRepository.GetPatientHistoryByUserIdAsync(userId);
        }

        public void Remove(PatientHistory entity)
        {
            _unitOfWork.PatientHistoryRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}