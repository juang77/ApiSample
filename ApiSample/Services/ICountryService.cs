using ApiSample.Models;

namespace ApiSample.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country> GetByIdAsync(int id);
    Task<int> CreateAsync(Country country);
    Task<bool> UpdateAsync(Country country);
    Task<bool> DeleteAsync(int id);
}
