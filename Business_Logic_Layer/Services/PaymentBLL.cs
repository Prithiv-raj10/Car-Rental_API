using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
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
        public PaymentBLL(IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }
        public Task<ActionResult<Status>> MakePayment(string userId)
        {
            var res = _paymentRepo.MakePayment(userId);
            return res;
        }
    }
}
