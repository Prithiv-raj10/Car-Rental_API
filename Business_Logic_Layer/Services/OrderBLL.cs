using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class OrderBLL: IOrderBLL
    {
        private readonly IOrderRepository _orderRepo;
        public OrderBLL(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Task<ActionResult<Status>> GetOrders(string? userId)
        {
            var res = _orderRepo.GetOrders(userId);
            return res;
        }

        public Task<ActionResult<Status>> GetOrdersById(int id)
        {
            var res = _orderRepo.GetOrdersById(id);
            return res;
        }
        public Task<ActionResult<Status>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            var res = _orderRepo.CreateOrder(orderHeaderDTO);
            return res;
        }
        public  Task<ActionResult<Status>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdateDTO)
        {
            var res = _orderRepo.UpdateOrderHeader(id,orderHeaderUpdateDTO);
            return res;
        }
    }
}
