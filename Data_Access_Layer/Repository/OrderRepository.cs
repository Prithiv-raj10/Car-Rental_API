
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
   public class OrderRepository:IOrderRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        protected Status _status;

        public OrderRepository(ApplicationDbContext context,ILogger<OrderRepository> logger)
        {
            this._context = context;
            _logger=logger;
            _status = new Status();

        }
        public async Task<ActionResult<Status>> GetOrders(string? userId)
        {
            try
            {
                var orderHeaders =
                    _context.OrderHeaders.Include(u => u.OrderDetails)
                    .ThenInclude(u => u.CarList)
                    .OrderByDescending(u => u.OrderHeaderId);

                _logger.LogDebug("order request initiated by user id {userId}", userId);

                if (!string.IsNullOrEmpty(userId))
                {
                    _status.Result= orderHeaders.Where(u => u.ApplicationUserId == userId);
                }
                else
                {
                    _status.Result = orderHeaders;
                }


                _status.StatusCode = (int)HttpStatusCode.OK;
                return (_status);
            }
            catch (Exception ex)
            {

                _status.StatusCode = (int)HttpStatusCode.BadRequest;
                _status.Message= ex.Message.ToString();
            }
            return _status;
        }


        public async Task<ActionResult<Status>> GetOrdersById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _status.StatusCode = (int)HttpStatusCode.NotFound;
                    return _status;
                }


                var orderHeaders = _context.OrderHeaders.Include(u => u.OrderDetails)
                    .ThenInclude(u => u.CarList)
                    .Where(u => u.OrderHeaderId == id);
                if (orderHeaders == null)
                {
                    _status.StatusCode = (int)HttpStatusCode.NotFound;
                    _status.Message = "Not Found";
                    _logger.LogInformation("Order with id {id} not found", id);
                    return (_status);
                }
                _status.Result = orderHeaders;
                _status.StatusCode = 1;
                return (_status);
            }
            catch (Exception ex)
            { 
                _status.StatusCode = (int)HttpStatusCode.BadRequest;
                _status.Message= ex.ToString() ;
            }
            return _status;
        }

        public async Task<ActionResult<Status>> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            try
            {
                OrderHeader order = new()
                {
                    ApplicationUserId = orderHeaderDTO.ApplicationUserId,
                    PickupEmail = orderHeaderDTO.PickupEmail,
                    PickupName = orderHeaderDTO.PickupName,
                    PickupPhoneNumber = orderHeaderDTO.PickupPhoneNumber,
                    OrderTotal = orderHeaderDTO.OrderTotal,
                    OrderDate = DateTime.Now,
                    StripePaymentIntentID = orderHeaderDTO.StripePaymentIntentID,
                    Status = String.IsNullOrEmpty(orderHeaderDTO.Status) ? SD.status_pending : orderHeaderDTO.Status,
                };

               
                    _context.OrderHeaders.Add(order);
                    _context.SaveChanges();
                    foreach (var orderDetailDTO in orderHeaderDTO.OrderDetailsDTO)
                    {
                        OrderDetails orderDetails = new()
                        {
                            OrderHeaderId = order.OrderHeaderId,
                            ItemName = orderDetailDTO.ItemName,
                            CarListId = orderDetailDTO.CarListId,
                            Price = orderDetailDTO.Price,
                           
                        };
                        _context.OrderDetails.Add(orderDetails);
                    }
                    _context.SaveChanges();
                    _status.Result = order;
                    order.OrderDetails = null;
                _status.StatusCode = (int)HttpStatusCode.Created;
                return (_status);
                
            }
            catch (Exception ex)
            {

                _status.StatusCode = (int)HttpStatusCode.BadRequest;
                _status.Message= ex.ToString() ;
            }
            return _status;
        }

        public async Task<ActionResult<Status>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO orderHeaderUpdateDTO)
        {
            try
            {
                if (orderHeaderUpdateDTO == null || id != orderHeaderUpdateDTO.OrderHeaderId)
                {
                    _status.StatusCode = (int)HttpStatusCode.NotFound;
                    return _status;
                }
                OrderHeader orderFromDb = _context.OrderHeaders.FirstOrDefault(u => u.OrderHeaderId == id);

                if (orderFromDb == null)
                {

                    _status.StatusCode = (int)HttpStatusCode.NotFound;
                    return _status;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdateDTO.PickupName))
                {
                    orderFromDb.PickupName = orderHeaderUpdateDTO.PickupName;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdateDTO.PickupPhoneNumber))
                {
                    orderFromDb.PickupPhoneNumber = orderHeaderUpdateDTO.PickupPhoneNumber;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdateDTO.PickupEmail))
                {
                    orderFromDb.PickupEmail = orderHeaderUpdateDTO.PickupEmail;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdateDTO.Status))
                {
                    orderFromDb.Status = orderHeaderUpdateDTO.Status;
                }
                if (!string.IsNullOrEmpty(orderHeaderUpdateDTO.StripePaymentIntentID))
                {
                    orderFromDb.StripePaymentIntentID = orderHeaderUpdateDTO.StripePaymentIntentID;
                }
                _context.SaveChanges();
                _status.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogInformation("Order updated for id {id}", id);
                return _status;



            }
            catch (Exception ex)
            {
                _status.StatusCode = (int)HttpStatusCode.BadRequest;
                
                _status.Message=  ex.ToString() ;
            }
            return _status;
        }
    }
}
