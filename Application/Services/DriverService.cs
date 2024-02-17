using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _iMapper;
        private readonly ILogger<DriverService> _logger;

        public DriverService(IDriverRepository driverRepository, IMapper iMapper, ILogger<DriverService> logger)
        {
            _driverRepository = driverRepository;
            _iMapper = iMapper;
            _logger = logger;
        }

        public async Task<DriverListDTO> GetDriverById(int id)
        {
            _logger.LogInformation("Attempting to retrieve driver with ID: {Id}", id);

            var driver = await _driverRepository.GetById(id);
            if (driver == null)
            {
                _logger.LogWarning("Driver with ID {Id} not found", id);
                return null;
            }

            _logger.LogInformation("Successfully retrieved driver with ID: {Id}", id);
            return _iMapper.Map<DriverListDTO>(driver);
        }

        public async Task<IEnumerable<DriverListDTO>> GetAllDrivers()
        {
            _logger.LogInformation("Attempting to retrieve all drivers");

            var drivers = await _driverRepository.GetAll();

            _logger.LogInformation("Successfully retrieved {Count} drivers", drivers.Count());

            return _iMapper.Map<IEnumerable<DriverListDTO>>(drivers);
        }

        public async Task<IEnumerable<DriverListDTO>> GetAllDriversAlphabetically()
        {
            _logger.LogInformation("Attempting to retrieve all drivers alphabetically");

            var drivers = await _driverRepository.GetAllAlphabetically();

            _logger.LogInformation("Successfully retrieved {Count} drivers alphabetically", drivers.Count());

            return _iMapper.Map<IEnumerable<DriverListDTO>>(drivers);
        }

        public async Task<DriverAlphabetizedDTO> GetAlphabetizedName(int id)
        {
            _logger.LogInformation("Attempting to retrieve alphabetized name for driver with ID: {Id}", id);

            var driver = await _driverRepository.GetById(id);
            if (driver == null)
            {
                _logger.LogWarning("Driver with ID {Id} not found", id);
                throw new KeyNotFoundException($"Driver with ID {id} not found");
            }

            var alphabetizedName = AlphabetizeName(driver.FirstName) + " " + AlphabetizeName(driver.LastName);

            _logger.LogInformation("Successfully retrieved alphabetized name for driver with ID: {Id}", id);

            return new DriverAlphabetizedDTO
            {
                Id = driver.Id,
                UserName = alphabetizedName,
                PhoneNumber = driver.PhoneNumber,
                Email = driver.Email
            };
        }

        public async Task AddDriver(CreateDriverDTO driverDTO)
        {
            _logger.LogInformation("Adding new driver");

            var driver = _iMapper.Map<Driver>(driverDTO);
            await _driverRepository.Add(driver);

            _logger.LogInformation("New driver added successfully");
        }

        public async Task UpdateDriver(UpdateDriverDTO driverDTO)
        {
            _logger.LogInformation("Updating driver with ID: {Id}", driverDTO.Id);

            var driver = _iMapper.Map<Driver>(driverDTO);
            await _driverRepository.Update(driver);

            _logger.LogInformation("Driver with ID {Id} updated successfully", driverDTO.Id);
        }

        public async Task DeleteDriver(int id)
        {
            // add validation here 
            
            var driver = await _driverRepository.GetById(id);
            if (driver == null)
            {
                _logger.LogWarning("Driver with ID {Id} not found", id);
                throw new KeyNotFoundException($"Driver with ID {id} not found");
            }
            
            
            _logger.LogInformation("Deleting driver with ID: {Id}", id);

            await _driverRepository.Delete(id);

            _logger.LogInformation("Driver with ID {Id} deleted successfully", id);
        }

        public async Task InsertRandomNames(int count)
        {
            _logger.LogInformation("Inserting {Count} random names", count);

            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var firstName = GenerateRandomName(random.Next(5, 10));
                var lastName = GenerateRandomName(random.Next(5, 10));
                var email = $"{firstName.ToLower()}_{lastName.ToLower()}@example.com";
                var phoneNumber = GenerateRandomPhoneNumber(random);
                var driver = new Driver { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };
                await _driverRepository.Add(driver);
            }

            _logger.LogInformation("{Count} random names inserted successfully", count);
        }

        private string GenerateRandomName(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateRandomPhoneNumber(Random random)
        {
            return string.Format("{0:000-000-0000}", random.Next(0, 999999999));
        }

        private string AlphabetizeName(string name)
        {
            int CustomCompare(char c1, char c2)
            {
                int result = Char.ToLower(c1).CompareTo(Char.ToLower(c2));
                return result == 0 ? c1.CompareTo(c2) : result;
            }
            char[] chars = name.ToCharArray();

            Array.Sort(chars, CustomCompare);

            return new string(chars);
        }
    }
}
