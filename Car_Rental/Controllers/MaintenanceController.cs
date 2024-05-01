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

        public MaintenanceController(IMaintenanceRepository maintenanceRepository) {
            this.maintenanceRepository = maintenanceRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Maintenance> maintenances = maintenanceRepository.getAll().Where(m=>m.IsDeleted==false).ToList();

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
              Message=maintenanceDTOs 

          };  
            return generalResponse;
        }
    }
}
