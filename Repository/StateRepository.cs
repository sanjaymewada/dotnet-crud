using CRUD_Demo.Data;
using CRUD_Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Demo.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;
        public StateRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<State>> GetByCountryId(int countryId)
        {
            return await _context.States.Where(s => s.CountryId == countryId).ToListAsync();
        }
    }
}
