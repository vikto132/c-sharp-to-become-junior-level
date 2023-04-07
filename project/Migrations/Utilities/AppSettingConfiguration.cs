using System;
using Microsoft.Extensions.Configuration;

namespace Migrations.Utilities;

public static class AppSettingConfiguration
{
    private const string Environment = "Environment";
    private const string Production = "Production";
    private const string Local = "Local";
        
    private static IConfigurationRoot _configuration;

    public static void Inject(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    public static bool IsProduction() => CheckEnvironment(Production);
        
    public static bool IsLocal() => CheckEnvironment(Local);

    private static bool CheckEnvironment(string expected)
    {
        if (_configuration == null) return false;
        var environment = _configuration[Environment];
        return !string.IsNullOrEmpty(environment) &&
               environment.Equals(expected, StringComparison.OrdinalIgnoreCase);
    }
}