﻿using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class BloodTestDataService : IBloodTestDataService
    {
        private readonly IBloodTestDataRepository _bloodTestDataRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BloodTestDataService(IBloodTestDataRepository bloodTestDataRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bloodTestDataRepository = bloodTestDataRepository;
        }

        public async Task<BloodTestData> Add(BloodTestData entity)
        {
            var record = await _unitOfWork.BloodTestDataRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task AddRangeAsync(List<BloodTestData> entities)
        {
            await _unitOfWork.BloodTestDataRepository.InsertRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }

        public Task Edit(BloodTestData entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BloodTestData>> GetBloodTestDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BloodTestData> GetBloodTestDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(BloodTestData entity)
        {
            throw new NotImplementedException();
        }
    }
}
