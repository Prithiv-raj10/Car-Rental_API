using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentBLL _payment;
        public PaymentController(IPaymentBLL payment)
        {
            _payment = payment;
        }

        [HttpPost]
        public async Task<ActionResult<Status>> MakePayment(string userId)
        {
            //make payment
            var res = _payment.MakePayment(userId);
            return Ok(res);
        }
    }
}
