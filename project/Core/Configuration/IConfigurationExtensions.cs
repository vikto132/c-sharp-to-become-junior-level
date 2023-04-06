using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    public static class IConfigurationExtensions
    {
        public static TOptions GetSection<TOptions>(this IConfiguration configuration, string key)
            where TOptions : new()
        {
            var options = new TOptions();
            configuration.GetSection(key).Bind(options);
            return options;
        }
    }
}