using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController: Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city != null)
            {
                return Ok(city.PointsOfInterest);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("{cityId}/pointsofinterest/{pointsofinterestid}")]
        public IActionResult GetPointOfInterest(int cityId, int pointsOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city != null)
            {
                var pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.Id == pointsOfInterestId);

                if (pointOfInterest != null)
                {
                    return Ok(pointOfInterest);
                }
                else
                {
                    return NotFound("Point of interest not found");
                }
            }
            else
            {
                return NotFound("City not found");
            }
        }
    }
}
