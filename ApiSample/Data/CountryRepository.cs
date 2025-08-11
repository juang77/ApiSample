using ApiSample.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace ApiSample.Data
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDbConnection _connection;

        public CountryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            await _connection.EnsureOpenAsync();

            var sql = "SELECT * FROM Countries ORDER BY Id";
            return await _connection.QueryAsync<Country>(sql);
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            await _connection.EnsureOpenAsync();

            var sql = "SELECT * FROM Countries WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Country>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(Country country)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"INSERT INTO Countries (Name) 
                        VALUES (@Name) 
                        RETURNING Id;";

            return await _connection.ExecuteScalarAsync<int>(sql, country);
        }

        public async Task<bool> UpdateAsync(Country country)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"UPDATE Countries SET Name = @Name WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, country);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _connection.EnsureOpenAsync();

            var sql = @"DELETE FROM Countries WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
