using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class PaymentBLL: IPaymentBLL
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly ILogger<PaymentBLL> _logger;
        public PaymentBLL(IPaymentRepository paymentRepo,ILogger<PaymentBLL> logger)
        {
            _paymentRepo = paymentRepo;
            _logger = logger;
        }
        public Task<ActionResult<Status>> MakePayment(string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to make a payment for user ID {userId}", userId);
                var res = _paymentRepo.MakePayment(userId);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making a payment for user ID {userId}", userId);
                throw new ArgumentException("Payment processing failed");

            }
        }
    }
}
