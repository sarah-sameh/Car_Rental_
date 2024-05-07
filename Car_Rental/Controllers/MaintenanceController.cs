using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceRepository maintenanceRepository;

        public MaintenanceController(IMaintenanceRepository maintenanceRepository)
        {
            this.maintenanceRepository = maintenanceRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Maintenance> maintenances = maintenanceRepository.getAll().Where(m => m.IsDeleted == false).ToList();

            List<MaintenanceDTO> maintenanceDTOs = maintenances.Select(c => new MaintenanceDTO
            {
                Type = c.Type,
                Cost = c.Cost,
                Description = c.Description,
                MaintenanceDate = c.MaintenanceDate,
            }).ToList();

            GeneralResponse generalResponse = new GeneralResponse()
            {
                IsPass = true,
                Message = maintenanceDTOs

            };
            return generalResponse;
        }
        [HttpPut("{id:int}")]
        public ActionResult<GeneralResponse> Update(int id, MaintenanceDTO maintenanceDTO)
        {
            Maintenance maintenance = maintenanceRepository.get(id);

            if (maintenance == null)
            {

                return new GeneralResponse()
                {
                    IsPass = false,
                    Message = "wrong id"
                };
            }
            maintenance.Cost = maintenanceDTO.Cost;
            maintenance.Description = maintenanceDTO.Description;
            maintenance.Type = maintenanceDTO.Type;
            maintenance.MaintenanceDate = maintenanceDTO.MaintenanceDate;

            return new GeneralResponse()
            {
                IsPass = true,
                Message = maintenanceDTO
            };

        }

        [HttpDelete]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            Maintenance maintenance = maintenanceRepository.get(id);
            if (maintenance == null)
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Invalid Id"
                };
            }

            maintenanceRepository.delete(id);
            return new GeneralResponse()
            {
                IsPass = true,
                Message = "Deleted Successfully"
            };

        }

        [HttpPost]
        public ActionResult<GeneralResponse> Insert(InsertMaintenanceDTO newMaintenance)
        {

            Maintenance maintenance = new Maintenance();
            maintenance.Cost = newMaintenance.Cost;
            maintenance.Description = newMaintenance.Description;
            maintenance.Type = newMaintenance.Type;
            maintenance.Car_Id = newMaintenance.Car_Id;
            maintenance.MaintenanceDate = newMaintenance.MaintenanceDate;

            maintenanceRepository.Insert(maintenance);
            maintenanceRepository.save();
            return new GeneralResponse()
            {
                IsPass = true,
                Message = newMaintenance
            };



        }
        [HttpGet("{id}")]
        public ActionResult<GeneralResponse> GetById(int id)
        {
            Maintenance maintenance = maintenanceRepository.get(id);

            if (maintenance == null || maintenance.IsDeleted)
            {
                GeneralResponse generalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Maintenance not found."
                };
                return NotFound(generalResponse);
            }

            MaintenanceDTO maintenanceDTO = new MaintenanceDTO
            {
                Cost = maintenance.Cost,
                Description = maintenance.Description,
                Type = maintenance.Type,
                MaintenanceDate = maintenance.MaintenanceDate
            };

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = maintenanceDTO
            };
            return response;
        }

    }
}
