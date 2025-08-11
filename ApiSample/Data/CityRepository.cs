using ApiSample.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace ApiSample.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbConnection _connection;

        public CityRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            await _connection.EnsureOpenAsync();

            var sql = "SELECT * FROM Cities ORDER BY Id";
            var result = await _connection.QueryAsync<City>(sql);
            return result;
        }

        public async Task<City> GetByIdAsync(int id)
        {
            await _connection.EnsureOpenAsync();

            var sql = "SELECT * FROM Cities WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<City>(sql, new { Id = id });
        }

        public async Task<IEnumerable<City>> GetByCountryIdAsync(int countryId)
        {
            await _connection.EnsureOpenAsync();

            var sql = "SELECT * FROM Cities WHERE CountryId = @CountryId";
            return await _connection.QueryAsync<City>(sql, new { CountryId = countryId });
        }

        public async Task<int> CreateAsync(City city)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"INSERT INTO Cities (Name, CountryId) 
                        VALUES (@Name, @CountryId) 
                        RETURNING Id;";

            return await _connection.ExecuteScalarAsync<int>(sql, city);
        }

        public async Task<bool> UpdateAsync(City city)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"UPDATE Cities SET Name = @Name, CountryId = @CountryId WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, city);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"DELETE FROM Cities WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
