using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<LocationDTO>> GetLocations()
        {
            var locations = _locationRepository.getAll();
            var locationDTOs = new List<LocationDTO>();

            foreach (var location in locations)
            {
                locationDTOs.Add(new LocationDTO
                {
                    Id = location.Id,
                    Name = location.Name,
                    Address = location.Address
                  
                });
            }

            return Ok(locationDTOs);
        }
        [HttpGet("cars/{address}")]
        public ActionResult<IEnumerable<CarDTO>> GetCarsByAddress(string address)
        {
            var cars = _locationRepository.getCarsByAddress(address);
            var carDTOs = new List<CarDTO>();

            foreach (var car in cars)
            {
                carDTOs.Add(new CarDTO
                {
                    Id = car.Id,
                    Model = car.Model,
                    Make = car.Make,
                    Year = car.Year,
                    FuelType = car.FuelType,
                    IsAvailable = car.IsAvailable,
                    Image = car.Image,
                    LocationId = car.Location_Id
                });
            }

            return Ok(carDTOs);
        }

        [HttpPost]
        public ActionResult CreateLocation(LocationDTO locationDTO)
        {
            var location = new Location
            {
                Name = locationDTO.Name,
                Address = locationDTO.Address
                
            };

            _locationRepository.Insert(location);
            _locationRepository.save();

            return CreatedAtAction(nameof(GetLocations), new { id = location.Id }, locationDTO);
        }
    }
}
