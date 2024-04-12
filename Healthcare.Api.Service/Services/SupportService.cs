using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupportService(ISupportRepository supportRepository, IUnitOfWork unitOfWork)
        {
            _supportRepository = supportRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Support> Add(Support entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Support entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Support> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Support>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(Support entity)
        {
            throw new NotImplementedException();
        }
    }
}
