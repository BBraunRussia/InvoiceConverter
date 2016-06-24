using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    public static class WorkWithString
    {
        public static string ReplaceSimbol(string str)
        {
            return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;");
        }

        public static string createString(params string[] Params)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Params.Length; i++)
                sb.Append(Params[i]);

            return sb.ToString();
        }
    }
}
