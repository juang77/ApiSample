﻿using System.Data;
using Npgsql;

namespace ApiSample.Data;

public static class DbConnectionExtensions
{
    public static async Task EnsureOpenAsync(this IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            await ((NpgsqlConnection)connection).OpenAsync();
    }
}
