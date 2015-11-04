
using System;
using System.Collections;
using System.Diagnostics;
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




            return FixIndents(result);
        }

        private static string FixIndents(string value)
        {
            var result = new StringBuilder();
            var lines = value.Split('\n');

            int pos = 0;
            foreach (var item in lines)
            {
                var thisPos = item.IndexOf(':');
                if (thisPos > pos)
                {
                    pos = thisPos;
                }
            }

            Trace.WriteLine("Pos: " + pos);

            foreach (var item in lines)
            {
                var colonPos = item.IndexOf(':');
                var newLine = item;
                if (colonPos > 0 && colonPos < pos)
                {
                    newLine = "".PadRight(pos - colonPos) + item;
                }
                result.Append(newLine);

            }

            return result.ToString();
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
