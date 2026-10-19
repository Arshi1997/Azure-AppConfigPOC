namespace AppConfigPOC.Models;

public class AppSettings
{
    public string ApplicationName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string DatabaseServer { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public int DatabasePort { get; set; }
    public string PrintServer { get; set; } = string.Empty;
    public bool EnablePrinting { get; set; }
    public string LogLevel { get; set; } = string.Empty;
    public string DatabasePassword { get; set; } = string.Empty;
}