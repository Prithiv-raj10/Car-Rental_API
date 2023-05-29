using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class BookingBLL: IBookingBLL
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ILogger<BookingBLL> _logger;
        public BookingBLL(IBookingRepository bookingRepo,ILogger<BookingBLL> logger)
        {
            _bookingRepo = bookingRepo;
            _logger = logger;
        }

        public Task<ActionResult<Status>> GetBooking(string userId)
        {
            try
            {
                _logger.LogInformation("Initiated booking by id {userId}", userId);
                var res = _bookingRepo.GetBooking(userId);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the booking");
                throw new ArgumentException("Get Booking failed");

            }
        }
        public Task<ActionResult<Status>> AddOrDelete(string userId, int carListId,int removeCar)
        {
           
            try
            {
                _logger.LogInformation("Booking has been updated");
                var res = _bookingRepo.AddOrDelete(userId, carListId, removeCar);
                return res;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the booking");
                throw new ArgumentException("An error occurred while updating");
            }
        }

    }
}
