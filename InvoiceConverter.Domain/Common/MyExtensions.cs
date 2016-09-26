using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Common
{
    public static class MyExtensions
    {
        public static string Cut(this string value, int lenght)
        {
            return (value.Length <= lenght) ? value : value.Substring(0, lenght);
        }

        public static string CutFromLeft(this string value, int lenght)
        {
            return (value.Length <= lenght) ? value : new string(value.Reverse().ToArray().Take(lenght).Reverse().ToArray());
        }

        public static string ReplaceEscape(this string value)
        {
            return value.Replace("'", "''");
        }

        public static string ReplaceSimbol(this string str)
        {
            return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;");
        }
    }
}
