using System;
using System.Text.RegularExpressions;

namespace ProjetoArtCouro.Mapping.Helpers
{
    internal static class ConvertValue
    {
        public static decimal ToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0.0M;
            }
            var regex = new Regex(@"[a-zA-Z][$]");
            var unscaledValue = regex.Replace(value, "").Trim().Replace(".", "");
            decimal.TryParse(unscaledValue, out decimal newValue);
            return newValue;
        }

        public static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            int.TryParse(value, out int newValue);
            return newValue;
        }

        public static DateTime ToDateTime(this string date)
        {
            return string.IsNullOrEmpty(date) ? new DateTime() : DateTime.ParseExact(date, "dd/MM/yyyy H:mm", null);
        }

        public static DateTime ToDateTimeWithoutHour(this string date)
        {
            return string.IsNullOrEmpty(date) ? new DateTime() : DateTime.ParseExact(date, "dd/MM/yyyy", null);
        }

        public static string ToFormatMoney(this decimal value)
        {
            return value.ToString("C");
        }
    }
}
