using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter
{
    public static class MyExtensions
    {
        public static string Cut(this string value, int lenght)
        {
            return (value.Length < lenght) ? value : value.Substring(0, lenght);
        }

        public static string ReplaceEscape(this string value)
        {
            return value.Replace("'", "''");
        }
    }
}
