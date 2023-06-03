using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBLL _order;
        public OrderController(IOrderBLL order)
        {
            _order = order;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Status>> GetOrders(string? userId)
        {
            var res = _order.GetOrders(userId);
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Status>> GetOrdersById(int id)
        {
            var res = _order.GetOrdersById(id);
            return Ok(res);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Status>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            var res = _order.CreateOrder(orderHeaderDTO);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Status>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdateDTO)
        {
            var res = _order.UpdateOrderHeader(id, orderHeaderUpdateDTO);
            return Ok(res);
        }
    }
}
