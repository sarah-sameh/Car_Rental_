using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IcarRepository _carRepository;

        public CarController(IcarRepository carRepository)
        {
            _carRepository = carRepository;
        }



        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Car> cars = _carRepository.getAll().Where(car => !car.IsDeleted).ToList();

            List<CarDTO> carDTOs = cars.Select(car => new CarDTO
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                Year = car.Year,
                FuelType = car.FuelType,
                IsAvailable = car.IsAvailable,
                Image = car.Image,
                LocationId = car.Location_Id
            }).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = carDTOs
            };

            return response;
        }

        [HttpGet("{id}")]
        public ActionResult<GeneralResponse> GetById(int id)
        {
            Car car = _carRepository.get(id);

            if (car == null || car.IsDeleted)
            {
                GeneralResponse generalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Car not found."
                };
                return NotFound(generalResponse);
            }

            CarDTO carDTO = new CarDTO
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                Year = car.Year,
                FuelType = car.FuelType,
                IsAvailable = car.IsAvailable,
                Image = car.Image,
                LocationId = car.Location_Id
            };

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = carDTO
            };
            return response;
        }



        [HttpGet("user/{id:guid}")]
        public ActionResult<GeneralResponse> GetCarsByUserId(Guid id)
        {
            List<Car> cars = _carRepository.GetCarsByUserId(id.ToString()).Where(c => !c.IsDeleted).ToList();

            if (cars == null || !cars.Any())
            {
                return NotFound("No cars found for the specified user ID.");
            }

            List<CarDTO> carDTOs = cars.Select(car => new CarDTO
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                Year = car.Year,
                FuelType = car.FuelType,
                IsAvailable = car.IsAvailable,
                Image = car.Image,
                LocationId = car.Location_Id
            }).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = carDTOs
            };

            return response;
        }

        [HttpPost]
        public ActionResult<GeneralResponse> Insert(CarDTO carDTO)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car
                {
                    Model = carDTO.Model,
                    Make = carDTO.Make,
                    Year = carDTO.Year,
                    FuelType = carDTO.FuelType,
                    IsAvailable = carDTO.IsAvailable,
                    Image = carDTO.Image,
                    Location_Id = carDTO.LocationId,
                    userId = carDTO.userID
                };

                _carRepository.Insert(car);
                _carRepository.save();

                return new GeneralResponse { IsPass = true, Message = "Car inserted successfully." };
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public ActionResult<GeneralResponse> Update(int id, CarDTO carDTO)
        {
            Car existingCar = _carRepository.get(id);

            if (existingCar == null)
            {
                GeneralResponse generalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Car not found."
                };
                return NotFound(generalResponse);
            }

            existingCar.Model = carDTO.Model;
            existingCar.Make = carDTO.Make;
            existingCar.Year = carDTO.Year;
            existingCar.FuelType = carDTO.FuelType;
            existingCar.IsAvailable = carDTO.IsAvailable;
            existingCar.Image = carDTO.Image;
            existingCar.Location_Id = carDTO.LocationId;

            _carRepository.Update(existingCar);

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = "Car updated successfully."
            };
            return response;
        }


        [HttpDelete("{id}")]
        public ActionResult<GeneralResponse> Delete(int id)
        {

            Car cars = _carRepository.get(id);
            _carRepository.delete(id);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = cars
            };

            return response;


        }

        [HttpGet("search")]
        public ActionResult<GeneralResponse> SearchByModel(string model)
        {
            List<Car> cars = _carRepository.SearchByMode(model);

            List<CarDTO> carDTO = cars.Select(car => new CarDTO
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                Year = car.Year,
                FuelType = car.FuelType,
                IsAvailable = car.IsAvailable,
                Image = car.Image,
                LocationId = car.Location_Id
            }).ToList();

            return new GeneralResponse()
            {
                IsPass = true,
                Message = carDTO
            };

        }

        [HttpGet("page")]
        public ActionResult<GeneralResponse> GetAll(int page)
        {
            List<Car> cars = _carRepository.getAll().Where(car => !car.IsDeleted).ToList();
            int pageSize = 10;
            nint currentPage = page;
            int totalCount = cars.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            List<CarDTO> carDTO = cars.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(car => new CarDTO
                {
                    Id = car.Id,
                    Model = car.Model,
                    Make = car.Make,
                    Year = car.Year,
                    FuelType = car.FuelType,
                    IsAvailable = car.IsAvailable,
                    Image = car.Image,
                    LocationId = car.Location_Id
                }).ToList();


            return new GeneralResponse()

            {
                IsPass = true,
                Message = carDTO

            };

        }

    }
}
