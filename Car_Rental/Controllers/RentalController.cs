using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {

        private readonly IRentalRepository rentaltRepository;

        public RentalController(IRentalRepository rentaltRepository)
        {
            this.rentaltRepository = rentaltRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Rental> rentals = rentaltRepository.getAll().Where(i => i.IsDeleted == false).ToList();

            return new GeneralResponse()
            {
                IsPass = true,
                Message = rentals
            };


        }

        [HttpGet("{id:guid}")]
        public ActionResult<GeneralResponse> GetByUserId(string id)
        {
            List<Rental> rentals = rentaltRepository.getByUserID(id).Where(i => i.IsDeleted == false).ToList();
            List<RentalDTO> rentalDTO = rentals.Select(r => new RentalDTO
            {
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                RentalRate = r.RentalRate,
                Car_Id = r.Car_Id,
                Customer_Id = r.Customer_Id,
                InsuranceCharge = r.InsuranceCharge,
                FuelCharge = r.FuelCharge
            }).ToList();


            return new GeneralResponse()
            {
                IsPass = true,
                Message = rentalDTO
            };

        }

        [HttpPost]
        public ActionResult<GeneralResponse> Add(RentalDTO rental)
        {
            if (ModelState.IsValid)
            {
                Rental dbrental = new Rental();
                dbrental.StartDate = rental.StartDate;
                dbrental.EndDate = rental.EndDate;
                dbrental.RentalRate = rental.RentalRate;
                dbrental.Car_Id = rental.Car_Id;
                dbrental.Customer_Id = rental.Customer_Id;
                dbrental.InsuranceCharge = rental.InsuranceCharge;
                dbrental.FuelCharge = rental.FuelCharge;

                rentaltRepository.Insert(dbrental);
                rentaltRepository.save();

                return new GeneralResponse()
                {
                    IsPass = true,
                    Message = dbrental
                };


            }
            return BadRequest(ModelState);
        }


        [HttpDelete]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            rentaltRepository.delete(id);
            rentaltRepository.save();
            return new GeneralResponse()
            {
                IsPass = true,
                Message = "deleted successfuly"
            };


        }



        [HttpPut("{id:int}")]
        public ActionResult<GeneralResponse> update(int id, RentalDTO rental)
        {

            Rental dbrental = rentaltRepository.get(id);
            if (dbrental == null)
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Not found"
                };

            }
            dbrental.StartDate = rental.StartDate;
            dbrental.EndDate = rental.EndDate;
            dbrental.RentalRate = rental.RentalRate;
            dbrental.Car_Id = rental.Car_Id;
            dbrental.Customer_Id = rental.Customer_Id;
            dbrental.InsuranceCharge = rental.InsuranceCharge;
            dbrental.FuelCharge = rental.FuelCharge;

            rentaltRepository.Update(dbrental);
            rentaltRepository.save();


            return new GeneralResponse()
            {
                IsPass = true,
                Message = dbrental
            };
        }
    }
}
