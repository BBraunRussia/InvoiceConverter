using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Mails
{
    public class MailToShow
    {
        public string Name
        {
            get
            {
                ICustomerRepository custRepo = CompositionRoot.Resolve<ICustomerRepository>();
                return custRepo.Customers.FirstOrDefault(c => c.ID == ID).Name;
            }
        }
        public int ID { set; get; }
        public DateTime Date { get; set; }
        public string FilePath { get; set; }
    }
}
