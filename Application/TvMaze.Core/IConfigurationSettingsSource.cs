namespace TvMaze.Core;

public interface IConfigurationSettingsSource
{
    string GetSetting(string key);
}