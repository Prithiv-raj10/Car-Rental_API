using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public  class PaymentRepository : IPaymentRepository
    {
        protected Status _status;
        private readonly IConfiguration _congifuration;
        private readonly ApplicationDbContext _context;
        public PaymentRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _congifuration = configuration;
            _context = context;
            _status = new();
        }

        
        public async Task<ActionResult<Status>> MakePayment(string userId)
        {
            Booking booking = _context.Bookings.Include(u => u.RentItems)
                .ThenInclude(u => u.CarList).FirstOrDefault(u => u.UserId == userId);

            if (booking == null || booking.RentItems == null ||booking.RentItems.Count() == 0)
            {
                _status.StatusCode = (int)HttpStatusCode.NotFound;
                return _status;
            }

            
            StripeConfiguration.ApiKey = _congifuration["StripeSettings:SecretKey"];
            booking.Total = (double)booking.RentItems.Sum(u => u.CarList.Rent);

            PaymentIntentCreateOptions options = new()
            {
                Amount = (int)(booking.Total * 100),
                Currency = "inr",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
            };
            PaymentIntentService service = new();
            PaymentIntent response = service.Create(options);
            booking.StripePaymentIntentId = response.Id;
            booking.ClientSecret = response.ClientSecret;

            _status.Result = booking;
            _status.StatusCode = (int)HttpStatusCode.OK;
            return _status;
        }
    }
}
