using TvMaze.Core;
using Microsoft.Extensions.Configuration;

namespace TvMaze.IoC;

public class ConfigurationExtensionsSource : IConfigurationSettingsSource
{
    private readonly IConfiguration _configuration;
	public ConfigurationExtensionsSource(IConfiguration configuration)
	{
        _configuration = configuration;
	}

    public string GetSetting(string key)
    {
        return _configuration[key];
    }
}

