using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter
{
    public static class WorkWithString
    {
        public static string ReplaceSimbol(string str)
        {
            return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;");
        }
    }
}
