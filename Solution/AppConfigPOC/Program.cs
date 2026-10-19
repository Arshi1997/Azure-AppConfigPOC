using AppConfigPOC.Services;

Console.WriteLine();
Console.WriteLine("==========================================");
Console.WriteLine("       App Configuration POC");
Console.WriteLine("==========================================");
Console.WriteLine();

try
{
    var configurationService = new ConfigurationService();

    var settings = configurationService.GetSettings();

    Console.WriteLine("Configuration loaded successfully.");
    Console.WriteLine();

    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Application Configuration");
    Console.WriteLine("------------------------------------------");

    Console.WriteLine(
        $"Application Name : {settings.ApplicationName}");

    Console.WriteLine(
        $"Environment      : {settings.Environment}");

    Console.WriteLine(
        $"Database Server  : {settings.DatabaseServer}");

    Console.WriteLine(
        $"Database Name    : {settings.DatabaseName}");

    Console.WriteLine(
        $"Database Port    : {settings.DatabasePort}");

    Console.WriteLine(
        $"Print Server     : {settings.PrintServer}");

    Console.WriteLine(
        $"Enable Printing  : {settings.EnablePrinting}");

    Console.WriteLine(
        $"Log Level        : {settings.LogLevel}");
    
    Console.WriteLine(
        $"Database Password : ");
        // $"Database Password : {settings.DatabasePassword}");

    Console.WriteLine("------------------------------------------");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("ERROR");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine(ex.Message);
    Console.WriteLine();
    Console.WriteLine(ex);
    
    Environment.ExitCode = 1;
}