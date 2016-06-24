using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
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

        public string GetItemAsDate(int index)
        {
            string str = GetItem(index);

            return (str.Length > 0) ? WorkWithString.CreateString(str.Substring(6, 2), ".", str.Substring(4, 2), ".", str.Substring(0, 4)) : str;
        }

        public string GetItem(int index)
        {
            return (_list.Count > index) ? _list[index] : string.Empty;
        }

        public void ConcValue(int index, string value)
        {
            _list[index] += " " + value;
        }
    }
}
