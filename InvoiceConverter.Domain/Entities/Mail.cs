using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Entities
{
    public class Mail
    {
        public int ID { get; set; }
        public int Customer_id { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
