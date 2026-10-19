namespace DynamicConfigAPI.Models;

public class DynamicSettings
{
    public string ApplicationName { get; set; } = string.Empty;

    public string Environment { get; set; } = string.Empty;

    public string WelcomeMessage { get; set; } = string.Empty;

    public bool MaintenanceMode { get; set; }

    public int MaxItems { get; set; }

    public string ApiKey { get; set; } = string.Empty;
}