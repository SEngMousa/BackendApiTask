using Application.DTOs;

namespace Application.Services;

public interface IDriverService
{
    Task<DriverListDTO> GetDriverById(int id);
    Task<DriverAlphabetizedDTO> GetAlphabetizedName(int id);

    Task<IEnumerable<DriverListDTO>> GetAllDrivers();
    Task<IEnumerable<DriverListDTO>> GetAllDriversAlphabetically();

    Task AddDriver(CreateDriverDTO driver);
    Task UpdateDriver(UpdateDriverDTO driver);
    Task DeleteDriver(int id);
    Task InsertRandomNames(int count);
}