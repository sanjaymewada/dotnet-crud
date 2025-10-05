using CRUD_Demo.Data;
using CRUD_Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_Demo.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Country>> GetAll() => await _context.Countries.ToListAsync();
    }
}
