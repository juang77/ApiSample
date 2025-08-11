using ApiSample.Data;
using ApiSample.Models;

namespace ApiSample.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var result = await _countryRepository.GetAllAsync();
            return result;
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Country country)
        {
            return await _countryRepository.CreateAsync(country);
        }

        public async Task<bool> UpdateAsync(Country country)
        {
            return await _countryRepository.UpdateAsync(country);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _countryRepository.DeleteAsync(id);
        }
    }
}
