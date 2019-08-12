using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController: Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var res = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
