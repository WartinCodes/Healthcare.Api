using Healthcare.Api.Core.Entities;
using System.Collections.Generic;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IStateRepository : IRepository<State>
    {
        Task<IEnumerable<State>> GetAllStateAsync();
        Task<IEnumerable<State>> GetStatesByCountryId(int countryId);
    }
}