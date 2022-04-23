using System.Globalization;

namespace SE3.Utils
{
    public static class Localization
    {
        public static void SetCountry(string country) => Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(country);
        public static string GetCountry() => Thread.CurrentThread.CurrentUICulture.Name;
    }
}
