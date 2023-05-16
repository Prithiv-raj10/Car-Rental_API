using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IBookingRepository
    {
        Task<ActionResult<Status>> GetBooking(string userId);
        Task<ActionResult<Status>> AddOrDelete(string userId, int carListId);
        
    }
}
