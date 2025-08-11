namespace ApiSample.Logging;

public interface IAwsStyleLogger
{
    void LogInfo(string message);
    void LogError(string message);
}
