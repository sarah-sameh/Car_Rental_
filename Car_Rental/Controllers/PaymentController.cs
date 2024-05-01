using Car_Rental.DTOs;
using Car_Rental.Models;
using Car_Rental.Repository;
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

        public PaymentController(UserManager<ApplicationUser> userManager, IPaymentRepository paymentRepository)
        {
            this.userManager = userManager;
            this.paymentRepository = paymentRepository;
        }


        [HttpGet]
        // [Authorize]
        public ActionResult<GeneralResponse> GetAllPayment()
        {
            List<Payment> payments = paymentRepository.getAll().Where(i => i.IsDeleted == false).ToList();

            var paymentsWithUserNames = paymentRepository.getAll()
              .Select(payment => new
              {
                  payment.Id,
                  payment.Date,
                  payment.Method,
                  payment.Amount,
                  payment.Customer_Id,
                  payment.IsDeleted,
              })
              .ToList();

            //return Ok(paymentsWithUserNames);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = paymentsWithUserNames
            };
            return response;
        }

        [HttpGet("{id:guid}")]
        public ActionResult<GeneralResponse> GetById(string id)
        {
            List<Payment> comments = paymentRepository.getByUserID(id).Where(i => i.IsDeleted == false).ToList();


            List<PaymentDTO> paymentDTOs = comments.Select(p => new PaymentDTO
            {
                Date = p.Date,
                Method = p.Method,
                Amount = p.Amount,
                userId = p.Customer_Id

            }).ToList();

            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = paymentDTOs
            };

            return response;
        }



        //[HttpGet("{username}")]
        //public ActionResult<GeneralResponse> GetAllByUserName(string username)
        //{
        //    var user = userManager.FindByNameAsync(username).Result;
        //    if (user == null)
        //    {
        //        return NotFound($"User '{username}' not found.");
        //    }
        //    var paymentsWithUserNames = paymentRepository.getAll()
        //    .Where(payment => payment.customer.UserName == username)
        //    .Select(payment => new
        //    {
        //        payment.Id,
        //        payment.Date,
        //        payment.Method,
        //        payment.Amount,

        //        CustomerName = payment.customer.UserName,
        //        CustomerEmail = payment.customer.Email
        //    })
        //    .ToList();
        //    GeneralResponse response = new GeneralResponse()
        //    {
        //        IsPass = true,
        //        Message = paymentsWithUserNames
        //    };
        //    return response;
        //}




        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> AddPayment(PaymentDTO paymentDTO)
        {

            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    Date = paymentDTO.Date,
                    Method = paymentDTO.Method,
                    Amount = paymentDTO.Amount,
                    Customer_Id = paymentDTO.userId
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
                    Message = "Cant Add Payment"
                };
                return localresponse;
            }
        }




        [HttpPut("{id:int}")]

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
            OldPayment.Date = updatedPayment.Date;
            OldPayment.Method = updatedPayment.Method;
            OldPayment.Amount = updatedPayment.Amount;


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
                    OldPayment.Amount

                }
            };
            return response;

        }


        [HttpDelete("{id:int}")]

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
