using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class UltrasoundImageService : IUltrasoundImageService
    {
        private readonly IUltrasoundImageRepository _ultrasoundImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UltrasoundImageService(IUltrasoundImageRepository ultrasoundImageRepository, IUnitOfWork unitOfWork)
        {
            _ultrasoundImageRepository = ultrasoundImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UltrasoundImage> Add(UltrasoundImage entity)
        {
            var record = await _unitOfWork.UltrasoundImageRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByStudyIdAsync(int studyId)
        {
            return (await _ultrasoundImageRepository.GetAsync())
                .Where(x => x.IdStudy == studyId)
                .AsEnumerable();
        }

        public async Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByUserIdAsync(int userId)
        {
            return await _ultrasoundImageRepository.GetUltrasoundImagesByUserId(userId);
        }

        public void Remove(UltrasoundImage entity)
        {
            _unitOfWork.UltrasoundImageRepository.Delete(entity);
            _unitOfWork.Save();
        }
    }
}
