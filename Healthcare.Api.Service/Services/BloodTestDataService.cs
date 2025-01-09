﻿using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Service.Helper;

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

        public async Task AddRangeAsync(int studyId, List<BloodTestData> entities)
        {
            var newDataLaboratories = new List<BloodTestData>();

            foreach (var item in entities)
            {
                var bloodTestData = await GetBloodTestDataByBloodTestIdAsync(item.IdBloodTest, studyId);

                if (bloodTestData != null)
                {
                    bloodTestData.Value = item.Value;
                    _unitOfWork.BloodTestDataRepository.Update(bloodTestData);
                }
                else
                {
                    item.IdStudy = studyId;
                    newDataLaboratories.Add(item);
                }
            }
            if (newDataLaboratories.Any())
            {
                await _unitOfWork.BloodTestDataRepository.InsertRangeAsync(newDataLaboratories);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task Edit(BloodTestData entity)
        {
            _unitOfWork.BloodTestDataRepository.Update(entity);
            _unitOfWork.Save();
        }

        public Task<IEnumerable<BloodTestData>> GetBloodTestDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodTestData?> GetBloodTestDataByBloodTestIdAsync(int bloodTestId, int studyId)
        {
            return (await _bloodTestDataRepository.GetByStudyIdAsync(studyId))
                .FirstOrDefault(x => x.IdBloodTest == bloodTestId);
        }

        public async Task<BloodTestData> GetBloodTestDataByIdAsync(int id)
        {
            return await _bloodTestDataRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdAsync(int studyId)
        {
            return await _bloodTestDataRepository.GetByStudyIdAsync(studyId);
        }

        public async Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdsAsync(int[] studiesIds)
        {
            var bloodDataTests = await _bloodTestDataRepository.GetBloodTestDatasByStudyIdsAsync(studiesIds);
            if (!bloodDataTests.Any())
            {
                return Enumerable.Empty<BloodTestData>();
            }

            var orderDict = BloodTestOrder.Order
                .Select((nombre, indice) => new { nombre, indice })
                .ToDictionary(x => x.nombre, x => x.indice);

            var orderData = bloodDataTests
                .OrderBy(b => orderDict.ContainsKey(b.BloodTest.OriginalName)
                    ? orderDict[b.BloodTest.OriginalName]
                    : int.MaxValue)
                .ToList();

            return orderData;
        }


        public void Remove(BloodTestData entity)
        {
            _unitOfWork.BloodTestDataRepository.Delete(entity);
            _unitOfWork.Save();
        }
    }
}
