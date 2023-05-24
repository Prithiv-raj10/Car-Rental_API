using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IBookingBLL
    {
        Task<ActionResult<Status>> GetBooking(string userId);

        Task<ActionResult<Status>> AddOrDelete(string userId, int carListId,int removeCar);
    }
}
