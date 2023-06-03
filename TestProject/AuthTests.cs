using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models.DTO;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Tests.Services
{
    [TestFixture]
    public class AuthBLLTests
    {
        private Mock<IAuthRepository> _authRepoMock;
        private Mock<ILogger<AuthBLL>> _loggerMock;
        private AuthBLL _authBLL;

        [SetUp]
        public void Setup()
        {
            _authRepoMock = new Mock<IAuthRepository>();
            _loggerMock = new Mock<ILogger<AuthBLL>>();
            _authBLL = new AuthBLL(_authRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Login_WithValidModel_ReturnsStatusFromAuthRepo()
        {
            // Arrange
            var model = new LoginRequestDTO
            {
                UserName = "testuser",
                Password = "password"
            };

            var status = new Status();

            _authRepoMock.Setup(repo => repo.Login(model))
                .ReturnsAsync(status);

            // Act
            var result = await _authBLL.Login(model);

            // Assert
            Assert.AreSame(status, result);
           
        }

        [Test]
        public async Task Login_WithException_ReturnsInternalServerErrorStatus()
        {
            // Arrange
            var model = new LoginRequestDTO
            {
                UserName = "testuser",
                Password = "password"
            };

            _authRepoMock.Setup(repo => repo.Login(model))
                .Throws(new ArgumentException("Some error occurred"));

            // Act
            var result = await _authBLL.Login(model);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual("An error occurred while processing the request", result.Message);
            
        }

        [Test]
        public async Task Register_WithValidModel_ReturnsStatusFromAuthRepo()
        {
            // Arrange
            var model = new RegisterRequestDTO
            {
                UserName = "testuser",
                Password = "password",
                Name = "Test User"
            };

            var status = new Status { StatusCode = (int)HttpStatusCode.OK,
            Message = "Registered successfully"};

            _authRepoMock.Setup(repo => repo.Register(model))
                .ReturnsAsync(status);

            // Act
            var result = await _authBLL.Register(model);

            // Assert
            Assert.AreSame(status, result);
           
        }

        [Test]
        public async Task Register_WithException_ReturnsInternalServerErrorStatus()
        {
            // Arrange
            var model = new RegisterRequestDTO
            {
                UserName = "testuser@gmail.com",
                Password = "password",
                Name = "Test User"
            };

            _authRepoMock.Setup(repo => repo.Register(model))
                .Throws(new ArgumentException("Some error occurred"));

            // Act
            var result = await _authBLL.Register(model);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual("An error occurred while processing the request", result.Message);
           
        }
    }
}
