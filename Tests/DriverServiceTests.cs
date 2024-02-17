using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Application.Services;
using Microsoft.Extensions.Logging;
namespace Tests
{
    [TestFixture]
    public class DriverServiceTests
    {
        private Mock<IDriverRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private IDriverService _driverService;
        private Mock<ILogger<DriverService>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IDriverRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<DriverService>>();

            _driverService = new DriverService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetDriverById_WithValidId_ReturnsDriver()
        {
            // Arrange
            int driverId = 1;
            var expectedDriver = new Driver { Id = driverId, FirstName = "Oliver", LastName = "Johnson" };
            _mockRepository.Setup(repo => repo.GetById(driverId)).ReturnsAsync(expectedDriver);
            _mockMapper.Setup(mapper => mapper.Map<DriverListDTO>(expectedDriver)).Returns(new DriverListDTO { Id = expectedDriver.Id, FirstName = expectedDriver.FirstName, LastName = expectedDriver.LastName, Email = expectedDriver.Email, PhoneNumber = expectedDriver.PhoneNumber});

            // Act
            var result = await _driverService.GetDriverById(driverId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDriver.Id, result.Id);
            Assert.AreEqual(expectedDriver.FirstName, result.FirstName);
            Assert.AreEqual(expectedDriver.LastName, result.LastName);
        }

        [Test]
        public async Task GetDriverById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidDriverId = 100;
            _mockRepository.Setup(repo => repo.GetById(invalidDriverId)).ReturnsAsync((Driver)null);

            // Act
            var result = await _driverService.GetDriverById(invalidDriverId);

            // Assert
            Assert.IsNull(result);
        }

       
        [Test]
        public async Task GetAllDriversAlphabetically_ReturnsDriversAlphabetically()
        {
            // Arrange
            var expectedDrivers = new List<Driver>
            {
                new Driver { Id = 1, FirstName = "Oliver", LastName = "Johnson" },
                new Driver { Id = 2, FirstName = "Oliver2", LastName = "Johnson2" },
                new Driver { Id = 3, FirstName = "Oliver3", LastName = "Johnson3" }
            };
            _mockRepository.Setup(repo => repo.GetAllAlphabetically()).ReturnsAsync(expectedDrivers);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<DriverListDTO>>(expectedDrivers))
                .Returns(expectedDrivers.Select(d => new DriverListDTO { Id = d.Id, FirstName = d.FirstName, LastName = d.LastName ,Email = d.Email, PhoneNumber = d.PhoneNumber}));

            // Act
            var result = await _driverService.GetAllDriversAlphabetically();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDrivers.Count, result.Count());
            CollectionAssert.AreEqual(expectedDrivers.Select(d => d.Id), result.Select(d => d.Id));
        }
        
        [Test]
        public async Task GetAlphabetizedName_WithValidId_ReturnsDriverAlphabetizedName()
        {
            // Arrange
            // Arrange
            int driverId = 1;
            var expectedDriver = new Driver { Id = driverId, FirstName = "Oliver", LastName = "Johnson", PhoneNumber = "123-456-7890", Email = "Oliver.Johnson@example.com" };
            _mockRepository.Setup(repo => repo.GetById(driverId)).ReturnsAsync(expectedDriver);

            // Act
            var result = await _driverService.GetAlphabetizedName(driverId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDriver.Id, result.Id);
            Assert.AreEqual("eilOrv hJnnoos", result.UserName);
            Assert.AreEqual(expectedDriver.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(expectedDriver.Email, result.Email);
        }

    
    }
}
