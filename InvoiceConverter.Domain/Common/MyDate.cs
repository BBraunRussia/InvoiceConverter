using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Common
{
    public static class MyDate
    {
        public static string GetDate(string value)
        {
            return (value.Length == 8) ? string.Concat(value.Substring(6, 2), ".", value.Substring(4, 2), ".", value.Substring(0, 4)) : value;
        }
    }
}
