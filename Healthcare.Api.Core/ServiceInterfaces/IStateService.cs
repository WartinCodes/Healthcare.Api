using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetAsync();
    }
}
