using System;

namespace VagabondK.Indicators
{
    static class DoubleExtensions
    {
        public static double ToValidValue(this double value, double defaultValue = 0d)
            => double.IsNaN(value) || double.IsInfinity(value) ? defaultValue : Math.Max(value, 0d);
    }
}
