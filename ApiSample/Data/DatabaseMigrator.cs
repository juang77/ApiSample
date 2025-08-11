using Dapper;
using Npgsql;
using System.Data;

namespace ApiSample.Data
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly IDbConnection _connection;

        public DatabaseMigrator(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task MigrateAsync()
        {
            if (_connection.State != ConnectionState.Open)
                await ((NpgsqlConnection)_connection).OpenAsync();

            var createCountriesTable = @"
                CREATE TABLE IF NOT EXISTS Countries (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL
                );";

            var createCitiesTable = @"
                CREATE TABLE IF NOT EXISTS Cities (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    CountryId INT NOT NULL REFERENCES Countries(Id) ON DELETE CASCADE
                );";

            var createLogsTable = @"
                CREATE TABLE IF NOT EXISTS ApiLogs (
                    Id SERIAL PRIMARY KEY,
                    Message TEXT NOT NULL,
                    LogLevel VARCHAR(50),
                    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );";

            await _connection.ExecuteAsync(createCountriesTable);
            await _connection.ExecuteAsync(createCitiesTable);
            await _connection.ExecuteAsync(createLogsTable);
        }
    }
}
