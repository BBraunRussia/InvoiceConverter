using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Lists
{
    public class ItemListDouble : ItemList
    {
        public override string GetItem(int index)
        {
            string value = base.GetItem(index);

            return value.Replace(".", ",");
        }
    }
}
