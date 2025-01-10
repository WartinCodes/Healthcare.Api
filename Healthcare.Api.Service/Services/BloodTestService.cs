using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Service.Helper;

namespace Healthcare.Api.Service.Services
{
    public class BloodTestService : IBloodTestService
    {
        private readonly IBloodTestRepository _bloodTestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BloodTestService(IBloodTestRepository bloodTestRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bloodTestRepository = bloodTestRepository;
        }

        public async Task<BloodTest> Add(BloodTest entity)
        {
            var record = await _unitOfWork.BloodTestRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(BloodTest entity)
        {
            _unitOfWork.BloodTestRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public async Task<BloodTest> GetBloodTestByIdAsync(int id)
        {
            return await _bloodTestRepository.GetBloodTestByIdAsync(id);
        }

        public async Task<BloodTest?> GetBloodTestByNamesAsync(string originalName, string parsedName)
        {
            return await _bloodTestRepository.GetBloodTestByNamesAsync(originalName, parsedName);
        }

        public void Remove(BloodTest entity)
        {
            _unitOfWork.BloodTestRepository.Remove(entity);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<BloodTest>> GetBloodTestsAsync()
        {
            var bloodTests = await _bloodTestRepository.GetAsync();
            if (!bloodTests.Any())
            {
                return Enumerable.Empty<BloodTest>();
            }

            var ordenDict = BloodTestOrder.Order
                .Select((nombre, indice) => new { nombre, indice })
                .ToDictionary(x => x.nombre, x => x.indice);

            var datosOrdenados = bloodTests
                .OrderBy(b => ordenDict.ContainsKey(b.OriginalName)
                    ? ordenDict[b.OriginalName]
                    : int.MaxValue)
                .ToList();

            return datosOrdenados;
        }

        public IEnumerable<string> GetAllBloodTestName()
        {
            return _bloodTestRepository.GetAsQueryable().Select(x => x.ParsedName).Distinct().ToList();
        }
    }
}
