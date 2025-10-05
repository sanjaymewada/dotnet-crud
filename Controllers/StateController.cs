using CRUD_Demo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_Demo.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepository;
        public StateController(IStateRepository stateRepository) => _stateRepository = stateRepository;

        [HttpGet]
        public async Task<IActionResult> GetStatesByCountry(int countryId)
        {
            var states = await _stateRepository.GetByCountryId(countryId);
            return Json(states);
        }
    }
}
