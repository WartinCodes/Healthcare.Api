using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IAddressService
    {
        IQueryable<Address> GetAsQueryable();
        Task<IEnumerable<Address>> GetAsync();
        Task<Address> Add(Address entity);
        void Remove(Address entity);
        void Edit(Address entity);
    }
}
