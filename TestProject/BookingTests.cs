using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Models.DTO;
using Data_Access_Layer.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Tests
{
    [TestFixture]
    public class AuthBLLTests
    {
        private Mock<IAuthRepository> _authRepoMock;
        private Mock<ILogger<AuthBLL>> _loggerMock;
        private IAuthBLL _authBLL;

        [SetUp]
        public void Setup()
        {
            _authRepoMock = new Mock<IAuthRepository>();
            _loggerMock = new Mock<ILogger<AuthBLL>>();
            _authBLL = new AuthBLL(_authRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Login_Successful()
        {
            // Arrange
            var model = new LoginRequestDTO {
                UserName = "testuser",
                Password = "password"
            };
            var status = new Status { StatusCode = (int)HttpStatusCode.OK, Message = "Login successful" };
            _authRepoMock.Setup(repo => repo.Login(model)).ReturnsAsync(status);

            // Act
            var result = await _authBLL.Login(model);

            // Assert
            Assert.AreEqual(status, result);
        }

        [Test]
        public async Task Login_ExceptionThrown()
        {
            // Arrange
            var model = new LoginRequestDTO
            {
                UserName = "testuser",
                Password = "password"
            };
            var exceptionMessage = "Some error occurred";
            _authRepoMock.Setup(repo => repo.Login(model)).Throws(new ArgumentException(exceptionMessage));

            // Act
            var result = await _authBLL.Login(model);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual("An error occurred while processing the request", result.Message);
        }

        [Test]
        public async Task Register_Successful()
        {
            // Arrange
            var model = new RegisterRequestDTO {
                UserName = "testuser",
                Password = "password"
            };
            var status = new Status { StatusCode = (int)HttpStatusCode.OK, Message = "Registered successfully" };
            _authRepoMock.Setup(repo => repo.Register(model)).ReturnsAsync(status);

            // Act
            var result = await _authBLL.Register(model);

            // Assert
            Assert.AreEqual(status, result);
        }

        [Test]
        public async Task Register_ExceptionThrown()
        {
            // Arrange
            var model = new RegisterRequestDTO {
                UserName = "testuser",
                Password = "password"
            };
            var exceptionMessage = "Some error occurred";
            _authRepoMock.Setup(repo => repo.Register(model)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _authBLL.Register(model);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual("An error occurred while processing the request", result.Message);
         
        }
    }
}
