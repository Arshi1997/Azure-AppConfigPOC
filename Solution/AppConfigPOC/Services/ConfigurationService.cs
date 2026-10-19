using Azure.Identity;
using AppConfigPOC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace AppConfigPOC.Services;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService()
    {
        var endpoint = Environment.GetEnvironmentVariable(
            "AZURE_APPCONFIG_ENDPOINT");

        var environment = Environment.GetEnvironmentVariable(
            "APP_ENVIRONMENT") ?? "dev";

        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new InvalidOperationException(
                "AZURE_APPCONFIG_ENDPOINT environment variable is not set.");
        }

        Console.WriteLine($"Azure App Configuration: {endpoint}");
        Console.WriteLine($"Environment: {environment}");

    var credential = new AzureCliCredential();

    var builder = new ConfigurationBuilder();

    builder.AddAzureAppConfiguration(options =>
    {
        options.Connect(
            new Uri(endpoint),
            credential);

        options.Select(
            "AppConfigPOC:*",
            LabelFilter.Null);

        options.Select(
            "AppConfigPOC:*",
            environment);

        options.ConfigureKeyVault(keyVault =>
        {
            keyVault.SetCredential(credential);
        });
    });

        _configuration = builder.Build();
    }

    public AppSettings GetSettings()
    {
        Console.WriteLine();
        Console.WriteLine("Loaded configuration keys:");
        Console.WriteLine("------------------------------------------");

        foreach (var item in _configuration.AsEnumerable())
        {
            Console.WriteLine($"{item.Key} = {item.Value}");
        }

        Console.WriteLine("------------------------------------------");

        var settings = new AppSettings();

        _configuration
            .GetSection("AppConfigPOC")
            .Bind(settings);

        return settings;
    }
}
