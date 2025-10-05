using CRUD_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Demo.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync (Employee employee);
        Task UpdateAsync (Employee employee);
        Task DeleteAsync (int id);

    }
}   
