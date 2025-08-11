namespace ApiSample.Logging
{
    public class AwsStyleLogger : IAwsStyleLogger
    {
        public void LogInfo(string message) =>
            Console.WriteLine($"[AWS INFO] {DateTime.UtcNow:o} - {message}");

        public void LogError(string message) =>
            Console.WriteLine($"[AWS ERROR] {DateTime.UtcNow:o} - {message}");
    }
}
