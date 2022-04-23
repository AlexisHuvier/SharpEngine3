using SE3.Utils;

namespace SE3Launcher.Utils
{
    internal class Settings
    {
        public string countryCode = Localization.GetCountry();

        public void Save() => JsonSave.SaveObj(this, "settings.json");
        public static Settings Load() => JsonSave.LoadObj<Settings>("settings.json");
    }
}
