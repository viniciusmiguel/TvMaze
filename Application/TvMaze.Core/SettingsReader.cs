namespace TvMaze.Core;

public class SettingsReader : ISettingsReader
{
    private readonly IConfigurationSettingsSource _configurationSettingsSource;
	public SettingsReader(IConfigurationSettingsSource configurationSettingsSource)
	{
        _configurationSettingsSource = configurationSettingsSource;
	}

    public string GetSetting(string settingName)
    {
        return _configurationSettingsSource.GetSetting(settingName) ?? throw new InvalidOperationException("Setting with name '" + settingName + "' not found");
    }

    public T GetSetting<T>(string settingName) where T : IConvertible
    {
        var setting = GetSetting(settingName);
        try
        {
            return (T) Convert.ChangeType(setting, typeof(T));
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unable to convert '" + settingName + "' to type: " + typeof(T),ex);
        }
    }
}

