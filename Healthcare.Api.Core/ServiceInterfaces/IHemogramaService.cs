using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IHemogramaService
    {
        Task<IEnumerable<Hemograma>> GetAsync();
        Task<Hemograma> GetHemogramaByIdAsync(int id);
        Task<Hemograma> Add(Hemograma entity);
        void Remove(Hemograma entity);
        void Edit(Hemograma entity);
    }
}
