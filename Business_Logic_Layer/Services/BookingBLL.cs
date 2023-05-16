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
    public class BookingBLL: IBookingBLL
    {
        private readonly IBookingRepository _bookingRepo;
        public BookingBLL(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public Task<ActionResult<Status>> GetBooking(string userId)
        {
            var res = _bookingRepo.GetBooking(userId);
            return res;
        }
        public Task<ActionResult<Status>> AddOrDelete(string userId, int carListId)
        {
            var res = _bookingRepo.AddOrDelete(userId, carListId);
            return res;
        }

    }
}
