using Azure;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        protected Status _status;
       
        public BookingRepository(ApplicationDbContext context)
        {
            this._context = context;
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
                _status.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return _status;
          
        }

        public async Task<ActionResult<Status>> AddOrDelete(string userId, int carListId)
        {
            Booking booking = _context.Bookings.Include(u => u.RentItems).FirstOrDefault(u => u.UserId == userId);
            CarList carList = _context.CarLists.FirstOrDefault(u => u.Id == carListId);
            if (carList == null)
            {
                _status.StatusCode = (int)HttpStatusCode.OK;
                _status.Message = "No car id found";
                return _status;
            }
            if (booking == null)
            {
                //create a shopping cart & add cart item

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
                _status.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {


                //RentItem cartItemInCart = booking.RentItems.FirstOrDefault(u => u.CarListId == carListId);

                //if (booking.RentItems.Count() == 1)
                //{
                //    _context.Bookings.Remove(booking);
                //}
                //_context.SaveChanges();
                _status.Message = "Previous Booking is pending, complete to book a new car";
                _status.StatusCode = (int)HttpStatusCode.OK;
            }
            return _status;

        }
    }
}
