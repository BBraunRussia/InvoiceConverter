using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string Body { get; set; }
        public string Contacts { get; set; }
        public bool Enable { get; set; }
    }
}
