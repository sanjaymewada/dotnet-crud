using CRUD_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Demo.Repository
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetByStateId(int stateId);
    }
}
