using ApiSample.Models;

namespace ApiSample.Services;

public interface ICityService
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City> GetByIdAsync(int id);
    Task<IEnumerable<City>> GetByCountryIdAsync(int countryId);
    Task<int> CreateAsync(City city);
    Task<bool> UpdateAsync(City city);
    Task<bool> DeleteAsync(int id);
}
