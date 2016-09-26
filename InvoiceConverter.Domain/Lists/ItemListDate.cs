using InvoiceConverter.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Lists
{
    public class ItemListDate : ItemList
    {
        public override string GetItem(int index)
        {
            string value = base.GetItem(index);

            return MyDate.GetDate(value);
        }
    }
}
