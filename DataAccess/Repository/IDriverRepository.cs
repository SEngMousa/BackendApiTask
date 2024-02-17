using Domain.Entities;

namespace DataAccess;

public interface IDriverRepository
{
    Task<Driver> GetById(int id);
    Task<IEnumerable<Driver>> GetAll();
    Task<IEnumerable<Driver>> GetAllAlphabetically();
    Task Add(Driver driver);
    Task Update(Driver driver);
    Task Delete(int id);
}