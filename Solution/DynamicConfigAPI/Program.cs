using Azure.Identity;
using DynamicConfigAPI.Models;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// Bootstrap configuration
// --------------------------------------------------

var endpoint = Environment.GetEnvironmentVariable(
    "AZURE_APPCONFIG_ENDPOINT");

var environment = Environment.GetEnvironmentVariable(
    "APP_ENVIRONMENT") ?? "dev";

if (string.IsNullOrWhiteSpace(endpoint))
{
    throw new InvalidOperationException(
        "AZURE_APPCONFIG_ENDPOINT environment variable is not set.");
}

Console.WriteLine(
    $"Azure App Configuration: {endpoint}");

Console.WriteLine(
    $"Environment: {environment}");


// --------------------------------------------------
// Authentication
// --------------------------------------------------

// Local POC authentication through `az login`
var credential = new AzureCliCredential();


// --------------------------------------------------
// Azure App Configuration
// --------------------------------------------------

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options
        .Connect(
            new Uri(endpoint),
            credential)

        // Only configuration belonging to this API
        .Select(
            "DynamicConfigAPI:*",
            environment)

        // Resolve Key Vault references
        .ConfigureKeyVault(keyVault =>
        {
            keyVault.SetCredential(credential);

            // Allows rotated Key Vault secrets
            // to be refreshed without restarting.
            keyVault.SetSecretRefreshInterval(
                TimeSpan.FromMinutes(1));
        })

        // Dynamic App Configuration refresh
        .ConfigureRefresh(refresh =>
        {
            refresh
                .RegisterAll()
                .SetRefreshInterval(
                    TimeSpan.FromSeconds(5));
        });
});


// --------------------------------------------------
// Services
// --------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddAzureAppConfiguration();

builder.Services.Configure<DynamicSettings>(
    builder.Configuration.GetSection(
        "DynamicConfigAPI"));


// CORS for our React UI
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


// --------------------------------------------------
// Middleware
// --------------------------------------------------

app.UseCors("Frontend");

// Important:
// Incoming HTTP requests trigger App Configuration
// refresh checks.
app.UseAzureAppConfiguration();

app.MapControllers();

app.Run();