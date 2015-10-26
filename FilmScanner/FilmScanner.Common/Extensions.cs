
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmScanner.Common
{
    public static partial class Extensions
    {

        /// <summary>
        /// Convert a string to int.  Will not throw exception if null or invalid.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Integer value of string or null if invalid/unparsable.</returns>
        public static int? ToIntSafe(this string value)
        {
            int? result = null;
            int innerResult;
            if (int.TryParse(value, out innerResult))
            {
                result = innerResult;
            }
            return result;
        }

        /// <summary>
        /// Converts to upper case but no exception is thrown if <paramref name="value"/> is null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUpperSafe(this string value)
        {
            return value == null ? null : value.ToUpper();
        }

        /// <summary>
        /// Returns true if string is null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Returns true if string is not null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return value.IsNullOrWhiteSpace() == false;
        }

        /// <summary>
        /// Returns true if string is numeric
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            float result;
            return float.TryParse(s, out result);
        }

        public static string Description(this TimeSpan ts)
        {
            var result = (string)null;

            switch (ts.Days)
            {
                case 0:
                    result = string.Format("{1} hours, {2} minutes", ts.Days, ts.Hours, ts.Minutes);
                    break;

                case 1:
                    result = string.Format("{0} day, {1} hours, {2} minutes", ts.Days, ts.Hours, ts.Minutes);
                    break;

                default:
                    result = string.Format("{0} days, {1} hours, {2} minutes", ts.Days, ts.Hours, ts.Minutes);
                    break;
            }

            //if (ts.Days == 0)
            //{
            //    result = string.Format("{1} hours, {2} minutes", ts.Days, ts.Hours, ts.Minutes);
            //}
            //else
            //{
            //    result = string.Format("{0} days, {1} hours, {2} minutes", ts.Days, ts.Hours, ts.Minutes);
            //}

            return result;
        }

    }

}
