namespace Infrastructure.Utilities;

public class MariaDbSettings
{
    public string? Server { get; set; }
    public string? Database { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }

    public string ConnectionString => $"server={Server};database={Database};User={User};password={Password};";
}