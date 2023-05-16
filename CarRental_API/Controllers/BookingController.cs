using Azure;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarRental_API.Controllers
{
    [Route("api/BookingController")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBLL _booking;
        public BookingController(IBookingBLL booking)
        {
            _booking = booking;
        }

        [HttpGet]
        public Task<ActionResult<Status>> GetBooking(string userId)
        {
            var res = _booking.GetBooking(userId);

            return res;
        }

        [HttpPost]
        public Task<ActionResult<Status>> AddOrDelete(string userId, int carListId)
        {

            var res = _booking.AddOrDelete(userId, carListId);
            return res;
        }
    }
}
