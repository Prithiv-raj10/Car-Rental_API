using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderBLL> _logger;
        public OrderBLL(IOrderRepository orderRepo, ILogger<OrderBLL> logger)
        {
            _orderRepo = orderRepo;
            _logger = logger;
        }
        public Task<ActionResult<Status>> GetOrders(string? userId)
        {
            
            try
            {
                _logger.LogInformation("Retrieving orders");
                var res = _orderRepo.GetOrders(userId);
                return res;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while retrieving orders for user: {userId}", userId);
                throw new ArgumentException("Error occurred while retrieving orders.", ex);
            }
        }

        public Task<ActionResult<Status>> GetOrdersById(int id)
        {
            
            try
            {
                _logger.LogInformation("Retrieving orders for ID: {id}", id);
                var res = _orderRepo.GetOrdersById(id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving orders for ID: {id}", id);
                throw new ArgumentException("Error occurred while retrieving orders by ID.", ex);
            }
        }
        public Task<ActionResult<Status>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            
            try
            {
                _logger.LogInformation("Creating order...");
                var res = _orderRepo.CreateOrder(orderHeaderDTO);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                throw new ArgumentException("Error occurred while creating the order.", ex);
            }
        }
        public  Task<ActionResult<Status>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdateDTO)
        {
            try
            {
                _logger.LogInformation("Updating order header with ID: {id}", id);

                var res = _orderRepo.UpdateOrderHeader(id, orderHeaderUpdateDTO);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order header with ID: {id}", id);
                throw new ArgumentException("Error occurred while updating the order header.", ex);
            }
        }
    }
}
