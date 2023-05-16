using Data_Access_Layer.DTO;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IOrderBLL
    {
        Task<ActionResult<Status>> GetOrders(string? userId);

        Task<ActionResult<Status>> GetOrdersById(int id);

        Task<ActionResult<Status>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO);

        Task<ActionResult<Status>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdateDTO);

    }
}
