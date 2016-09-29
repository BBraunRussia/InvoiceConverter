using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Mails
{
    public class MailsToShow
    {
        public IQueryable<MailToShow> Mails { get; private set; }
        
        public MailsToShow()
        {
            IMailRepository mailRepo = CompositionRoot.Resolve<IMailRepository>();
            ICustomerRepository custRepo = CompositionRoot.Resolve<ICustomerRepository>();

            Mails = from item in mailRepo.Mails
                    select new MailToShow{
                        ID = item.Customer_id,
                        Date = item.Date,
                        FilePath = item.FilePath
                    };
        }
    }
}
