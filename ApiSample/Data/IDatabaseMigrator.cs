namespace ApiSample.Data;

public interface IDatabaseMigrator
{
    Task MigrateAsync();
}
