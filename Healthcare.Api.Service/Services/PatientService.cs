﻿using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Patient> Add(Patient entity)
        {
            var record = await _unitOfWork.PatientRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(Patient entity)
        {
            _unitOfWork.PatientRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Patient> GetAsQueryable()
        {
            return _patientRepository.GetAsQueryable();
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await _patientRepository.GetAsync();
        }

        public Task<Patient> GetPatientByUserIdAsync(int userId)
        {
            return _patientRepository.GetPatientByUserIdAsync(userId);
        }

        public Task<Patient> GetPatientByIdAsync(int id)
        {
            return _patientRepository.GetPatientByIdAsync(id);
        }

        public void Remove(Patient entity)
        {
            _unitOfWork.PatientRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
