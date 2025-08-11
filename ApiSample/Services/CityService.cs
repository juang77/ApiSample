using ApiSample.Data;
using ApiSample.Models;

namespace ApiSample.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<City>> GetByCountryIdAsync(int countryId)
        {
            return await _cityRepository.GetByCountryIdAsync(countryId);
        }

        public async Task<int> CreateAsync(City city)
        {
            return await _cityRepository.CreateAsync(city);
        }

        public async Task<bool> UpdateAsync(City city)
        {
            return await _cityRepository.UpdateAsync(city);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _cityRepository.DeleteAsync(id);
        }
    }
}
