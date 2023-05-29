 using Azure;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class BookingRepository: IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingRepository> _logger;
        protected Status _status;
       
        public BookingRepository(ApplicationDbContext context, ILogger<BookingRepository> logger)
        {
            this._context = context;
            _logger = logger;
            _status = new Status();
            
        }
        public async Task<ActionResult<Status>> GetBooking(string userId)
        {

            try
            {
                Booking booking;
                if (string.IsNullOrEmpty(userId))
                {
                    booking = new();
                }
                else
                {
                    booking = _context.Bookings.Include(u=>u.RentItems).ThenInclude(u=>u.CarList).FirstOrDefault(u => u.UserId == userId);

                }

                if(booking==null)
                {
                    _status.StatusCode=(int)HttpStatusCode.OK;
                    _status.Message = "No booking done so far.";
                    _logger.LogInformation("No booking done");
                }
                else if (booking.RentItems != null && booking.RentItems.Count > 0)
                {
                    booking.Total = (double)booking.RentItems.Sum(u => u.CarList.Rent);
                }


                _status.Result = booking;
                return (_status);

            }
            catch (Exception ex)
            {

                _status.Message = "Error";
                _logger.LogError("Error on processing the request");
                _status.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return _status;
          
        }

        public async Task<ActionResult<Status>> AddOrDelete(string userId, int carListId, int removeCar)
        {
            Booking booking = _context.Bookings.Include(u => u.RentItems).FirstOrDefault(u => u.UserId == userId);
            CarList carList = _context.CarLists.FirstOrDefault(u => u.Id == carListId);
            if (carList == null)
            {
                _status.StatusCode = (int)HttpStatusCode.OK;
                _status.Message = "No car id found";
                _logger.LogInformation("No car with id {carListId} found", carListId);
                return _status;
            }
            if (booking == null)
            {
                //create a booking

                Booking newbooking = new() { UserId = userId };
                _context.Bookings.Add(newbooking);
                _context.SaveChanges();

                RentItem newRentItem = new()
                {
                    CarListId = carListId,
                    BookingId = newbooking.Id,
                    CarList = null
                };
                _context.RentItems.Add(newRentItem);
                _context.SaveChanges();
                _status.Message = "Car Booked Successfully";
                _logger.LogInformation("Car booked Successfully");
                _status.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {


                RentItem cartItemInCart = booking.RentItems.FirstOrDefault(u => u.CarListId == carListId);

                if (booking.RentItems.Count() == 1 && removeCar == 1)
                {
                    _context.Bookings.Remove(booking);
                    _context.RentItems.Remove(cartItemInCart);
                    _status.Message = "Removed Car";
                    _logger.LogInformation("Removed car with id {carListId}", carListId);
                    _status.StatusCode= (int)HttpStatusCode.OK;
                }
                else
                {
                    _status.Message = "Previous Booking is pending, complete to book a new car";
                    _logger.LogInformation("Previous Booking is pending with user id {userId}", userId);
                    _status.StatusCode = (int)HttpStatusCode.OK;
                }
                _context.SaveChanges();
            }
            return _status;





        }
    }
}
