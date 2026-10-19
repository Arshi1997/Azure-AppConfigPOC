using DynamicConfigAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DynamicConfigAPI.Controllers;

[ApiController]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IOptionsSnapshot<DynamicSettings> _settings;

    public ConfigController(
        IOptionsSnapshot<DynamicSettings> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public IActionResult GetConfiguration()
    {
        var settings = _settings.Value;

        return Ok(new
        {
            applicationName =
                settings.ApplicationName,

            environment =
                settings.Environment,

            welcomeMessage =
                settings.WelcomeMessage,

            maintenanceMode =
                settings.MaintenanceMode,

            maxItems =
                settings.MaxItems,

            // Never expose the actual secret.
            apiKeyLoaded =
                !string.IsNullOrWhiteSpace(
                    settings.ApiKey),

            loadedAt =
                DateTime.UtcNow
        });
    }
}