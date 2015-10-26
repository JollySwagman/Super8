
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FilmScanner.Common
{
    public static partial class Extensions
    {

        const int MAX_STRING_LENGTH = 100;

        /// <summary>
        /// Generic ToString(), can't handle everything hence try block
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToStringGeneric(this object o)
        {
            string result = "";
            try
            {
                if (o != null)
                {
                    var props = from p in o.GetType().GetProperties() select string.Format("{0}: {1}", p.Name, FormatProperty(p.GetValue(o, null), p));
                    result = "[" + o.GetType().FullName + "]" +
                            Environment.NewLine +
                            string.Join(Environment.NewLine, props) +
                            Environment.NewLine;
                }
            }
            catch (Exception)
            {
                // do nothing
            }
            return result;
        }

        /// <summary>
        /// Format property, handle enumerable types
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private static string FormatProperty(object o, PropertyInfo propertyInfo)
        {
            StringBuilder result = new StringBuilder();

            if (propertyInfo.PropertyType.IsPublic || propertyInfo.PropertyType.IsEnum)
            {
                var enumerable = o as IEnumerable;

                if (enumerable != null && o.GetType() != typeof(string))
                {
                    foreach (var item in enumerable)
                    {
                        result.Append(item.ToStringGeneric());
                    }
                }
                else
                {
                    if (o != null)
                    {
                        string displayString = o.ToString();
                        if (o.ToString().Length <= MAX_STRING_LENGTH)
                        {
                            result.Append(displayString);
                        }
                        else
                        {
                            result.Append(displayString.Substring(0, MAX_STRING_LENGTH) + "...");
                        }
                    }
                    else
                    {
                        result.Append("[null]");
                    }
                }
            }

            return result.ToString();
        }
    }
}
