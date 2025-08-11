using ApiSample.Logging;
using Dapper;
using Npgsql;
using System.Data;


namespace ApiSample.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAwsStyleLogger _awsLogger;
    private readonly IDbConnection _connection;

    public LoggingMiddleware(RequestDelegate next, IAwsStyleLogger awsLogger, IDbConnection connection)
    {
        _next = next;
        _awsLogger = awsLogger;
        _connection = connection;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logMessage = $"Request: {context.Request.Method} {context.Request.Path}";

        // Log AWS style
        _awsLogger.LogInfo(logMessage);

        // Log in DB
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        await _connection.ExecuteAsync(
            "INSERT INTO ApiLogs (Message, LogLevel) VALUES (@Message, @LogLevel)",
            new { Message = logMessage, LogLevel = "INFO" });

        await _next(context);
    }
}
