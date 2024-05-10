using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPaymentRepository paymentRepository;
        private readonly IMaintenanceRepository maintenanceRepository;

        public PaymentController(UserManager<ApplicationUser> userManager, IPaymentRepository paymentRepository, IMaintenanceRepository maintenanceRepository)
        {
            this.userManager = userManager;
            this.paymentRepository = paymentRepository;
            this.maintenanceRepository = maintenanceRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GeneralResponse>> GetAllPayment()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("User not authenticated.");
            }
            List<Payment> payments = paymentRepository.getAll().Where(i => i.IsDeleted == false).ToList();

            var paymentsWithUserNames = paymentRepository.getAll()
              .Select(payment => new
              {
                  payment.Id,
                  payment.Date,
                  payment.Method,
                  payment.Amount,
                  payment.Customer_Id,
                  CustomerName = currentUser.UserName,
                  CustomerEmail = currentUser.Email,

                  payment.IsDeleted
              })
              .ToList();


            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = paymentsWithUserNames
            };
            return response;
        }



        [HttpGet("{username}")]
        [Authorize]
        public ActionResult<GeneralResponse> GetAllByUserName(string username)
        {
            var user = userManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                return NotFound($"User '{username}' not found.");
            }
            var paymentsWithUserNames = paymentRepository.getAll()
            .Where(payment => payment.customer.UserName == username)
            .Select(payment => new
            {
                payment.Id,
                payment.Date,
                payment.Method,
                payment.Amount,

                CustomerName = payment.customer.UserName,
                CustomerEmail = payment.customer.Email
            })
            .ToList();
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = paymentsWithUserNames
            };
            return response;
        }


        [HttpPost]
        [Authorize]

        public async Task<ActionResult<GeneralResponse>> AddPayment(PaymentDTO paymentDTO)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("User not authenticated.");
            }
            if (ModelState.IsValid)
            {

                var maintenanceCost = maintenanceRepository.GetMaintenanceCost(paymentDTO.MaintenanceId);


                var totalAmount = maintenanceCost;

                var payment = new Payment
                {
                    Date = paymentDTO.Date,
                    Method = paymentDTO.Method,
                    Amount = totalAmount,
                    customer = currentUser,
                    Customer_Id = currentUser.Id,

                    Maintenance = maintenanceRepository.get(paymentDTO.MaintenanceId)
                };

                paymentRepository.Insert(payment);
                paymentRepository.save();

                return new GeneralResponse
                {
                    IsPass = true,
                    Message = new
                    {
                        payment.Id,
                        payment.Method,
                        payment.Amount
                    }
                };
            }
            else
            {
                GeneralResponse localresponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Can't Add Payment"
                };
                return localresponse;
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]

        public ActionResult<GeneralResponse> Edit(int id, PaymentDTO updatedPayment)
        {
            Payment OldPayment = paymentRepository.get(id);
            if (OldPayment == null)
            {
                GeneralResponse LocalResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Not Found"
                };
                return LocalResponse;
            }
            Maintenance maintenance = maintenanceRepository.get(updatedPayment.MaintenanceId);
            OldPayment.Date = updatedPayment.Date;
            OldPayment.Method = updatedPayment.Method;
            //  OldPayment.Amount = updatedPayment.Amount;
            OldPayment.Amount = maintenance.Cost;

            OldPayment.Maintenance = maintenanceRepository.get(updatedPayment.MaintenanceId);

            paymentRepository.Update(OldPayment);
            paymentRepository.save();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = new
                {
                    OldPayment.Id,
                    OldPayment.Date,
                    OldPayment.Method,
                    OldPayment.Amount,
                    MaintenanceId = OldPayment.Maintenance?.Id

                }
            };
            return response;

        }


        [HttpDelete("{id:int}")]
        [Authorize]


        public ActionResult<GeneralResponse> Remove(int id)
        {
            paymentRepository.delete(id);
            paymentRepository.save();
            GeneralResponse localresponse = new GeneralResponse()
            {
                IsPass = true,
                Message = "Done"
            };
            return localresponse;


        }
    }
}
