using CRUD_Demo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_Demo.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository) => _cityRepository = cityRepository;

        [HttpGet]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _cityRepository.GetByStateId(stateId);
            return Json(cities);
        }
    }
}
