namespace Infrastructure.Utilities;

public class PostgreSqlSettings
{
    public string? Host { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string ConnectionString => $"Host={Host};Database={Database};Username={Username};Password={Password};";
}