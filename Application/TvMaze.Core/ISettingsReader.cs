namespace TvMaze.Core;

public interface ISettingsReader
{
    string GetSetting(string settingName);
    T GetSetting<T>(string settingName) where T : IConvertible;
}