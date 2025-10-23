using System.Globalization;

namespace VagabondK.Indicators.Razor
{
    static class InvariantCultureStringExtensions
    {
        public static string ToSvgString(this double value) => value.ToString("R", CultureInfo.InvariantCulture);
    }
}
