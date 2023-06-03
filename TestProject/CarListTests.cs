using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Business_Logic_Layer.Tests
{
    [TestFixture]
    public class CarListBLLTests
    {
        private Mock<ICarListRepository> _carListRepoMock;
        private Mock<IWebHostEnvironment> _hostEnvironmentMock;
        private Mock<ILogger<CarListBLL>> _loggerMock;
        private CarListBLL _carListBLL;

        [SetUp]
        public void Setup()
        {
            _carListRepoMock = new Mock<ICarListRepository>();
            _hostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _loggerMock = new Mock<ILogger<CarListBLL>>();
            _carListBLL = new CarListBLL(_carListRepoMock.Object, _hostEnvironmentMock.Object, _loggerMock.Object);
        }

        [Test]
        public void GetAll_ReturnsListOfCars()
        {
            // Arrange
            var expectedCars = new List<CarList> { new CarList { Id = 1, Name = "Sample" , Category="Normal",SpecialTag="EV",Rent=1000 } };
            _carListRepoMock.Setup(repo => repo.GetAllCars()).Returns(expectedCars);

            // Act
            var result = _carListBLL.GetAll();

            // Assert
            Assert.AreEqual(expectedCars, result.Value);
        }

        [Test]
        public void GetCarById_ValidId_ReturnsCar()
        {
            // Arrange
            int validId = 1;
            var expectedCar = new CarList { Id = validId, Name = "Sample" };
            _carListRepoMock.Setup(repo => repo.GetCarById(validId)).Returns(expectedCar);

            // Act
            var result = _carListBLL.GetCarById(validId);

            // Assert
            Assert.AreEqual(expectedCar, result.Value);
        }


        [Test]
        public void Update_ValidIdAndModel_ReturnsTrue()
        {
            // Arrange
            int validId = 1;
            var model = new CarListUpdateDTO { Name = "Sample", Category = "Normal", SpecialTag = "EV", Rent = 1000 };

            _carListRepoMock.Setup(repo => repo.Update(validId, model)).ReturnsAsync(true);

            // Act
            var result = _carListBLL.Update(validId, model);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveCarById_ValidId_ReturnsTrue()
        {
            // Arrange
            int validId = 1;

            _carListRepoMock.Setup(repo => repo.RemoveCarById(validId)).Returns(true);

            // Act
            var result = _carListBLL.RemoveCarById(validId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
