using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Lists
{
    public class ItemList
    {
        private List<string> _list;

        public ItemList()
        {
            _list = new List<string>();
        }

        public int Count { get { return _list.Count; } }

        public void Add(string item)
        {
            _list.Add(item);
        }
        
        public string GetItem(int index, int maxLength)
        {
            string str = GetItem(index);
            if (str.Length > 120)
                str = str.Substring(0, 120);

            return str;
        }
        
        public virtual string GetItem(int index)
        {
            return (_list.Count > index) ? _list[index] : string.Empty;
        }

        public void ConcValue(int index, string value)
        {
            _list[index] += " " + value;
        }
    }
}
