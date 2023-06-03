using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Tests
{
    [TestFixture]
    public class PaymentBLLTests
    {
        private Mock<IPaymentRepository> _paymentRepoMock;
        private Mock<ILogger<PaymentBLL>> _loggerMock;
        private IPaymentBLL _paymentBLL;

        [SetUp]
        public void Setup()
        {
            _paymentRepoMock = new Mock<IPaymentRepository>();
            _loggerMock = new Mock<ILogger<PaymentBLL>>();
            _paymentBLL = new PaymentBLL(_paymentRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task MakePayment_ValidUserId_ReturnsStatusOK()
        {
            // Arrange
            string validUserId = "123";
            var status = new ActionResult<Status>(new Status
            {
                StatusCode = (int)HttpStatusCode.OK
            });

            _paymentRepoMock.Setup(repo => repo.MakePayment(validUserId)).ReturnsAsync(status);

            // Act
            var result = await _paymentBLL.MakePayment(validUserId);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result.Value.StatusCode);
        }

        [Test]
        public void MakePayment_RepositoryThrowsException_ThrowsArgumentException()
        {
            // Arrange
            string invalidUserId = "456";
            _paymentRepoMock.Setup(repo => repo.MakePayment(invalidUserId))
                .Throws(new Exception("Some error occurred"));

           
            Assert.ThrowsAsync<ArgumentException>(() => _paymentBLL.MakePayment(invalidUserId));
        }
    }
}
