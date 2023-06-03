using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Tests
{
    [TestFixture]
    public class OrderBLLTests
    {
        private Mock<IOrderRepository> _orderRepoMock;
        private Mock<ILogger<OrderBLL>> _loggerMock;
        private IOrderBLL _orderBLL;

        [SetUp]
        public void Setup()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _loggerMock = new Mock<ILogger<OrderBLL>>();
            _orderBLL = new OrderBLL(_orderRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetOrders_ValidUserId_ReturnsActionResultWithStatus()
        {
            // Arrange
            string userId = "123";

            var expectedResult = new ActionResult<Status>(new Status
            {
                StatusCode = 200,
                Message = "Orders retrieved successfully"
            });

            _orderRepoMock.Setup(repo => repo.GetOrders(userId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _orderBLL.GetOrders(userId);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetOrders_ExceptionThrown_LogsErrorAndThrowsArgumentException()
        {
            // Arrange
            string userId = "123";

            var expectedException = new Exception("Some error occurred");

            _orderRepoMock.Setup(repo => repo.GetOrders(userId))
                .Throws(expectedException);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _orderBLL.GetOrders(userId));
            
        }


        [Test]
        public async Task CreateOrder_ValidOrderHeader_ReturnsActionResultWithStatus()
        {
            // Arrange
            var orderHeaderDTO = new OrderHeaderCreateDTO
            {
                // Set up properties of orderHeaderDTO
                PickupName="TestName",
                PickupPhoneNumber="9565633225",
                PickupEmail="Test@example.com"
            };

            var expectedResult = new ActionResult<Status>(new Status
            {
                StatusCode = 200,
                Message = "Order created successfully"
            });

            _orderRepoMock.Setup(repo => repo.CreateOrder(orderHeaderDTO))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _orderBLL.CreateOrder(orderHeaderDTO);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CreateOrder_ExceptionThrown_LogsErrorAndThrowsArgumentException()
        {
            // Arrange
            var orderHeaderDTO = new OrderHeaderCreateDTO
            {
                PickupName = "TestName",
                PickupPhoneNumber = "9565633225",
                PickupEmail = "Test@example.com"
            };

            var expectedException = new Exception("Some error occurred");

            _orderRepoMock.Setup(repo => repo.CreateOrder(orderHeaderDTO))
                .Throws(expectedException);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _orderBLL.CreateOrder(orderHeaderDTO));

        }

    }
}
