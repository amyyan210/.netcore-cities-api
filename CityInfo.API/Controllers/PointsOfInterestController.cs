using System;
using System.Linq;
using CityInfo.API.Models;
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

        [HttpPost("{cityId}/pointsofinterest", Name = "GetPointOfInterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if(pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
            c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDTO()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                id = finalPointOfInterest.Id,
                finalPointOfInterest
            }
            );
        }
    }
}
