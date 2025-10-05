using CRUD_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Demo.Repository
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetByCountryId(int countryId);
    }
}
