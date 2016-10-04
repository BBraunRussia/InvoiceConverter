using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;
using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Mails
{
    public class MailToCustomer
    {
        private Customer customer;
        private IMailRepository mailRepository;

        public MailToCustomer(Customer customer)
        {
            mailRepository = CompositionRoot.Resolve<IMailRepository>();

            this.customer = customer;
        }
        
        public void SendMail(string filePath)
        {
            Sender.SendMail(customer.Recipient, customer.Subject, customer.Body, filePath);
        }
        
        public void SaveMail(string filePath)
        {
            Mail mail = new Mail();
            mail.Customer_id = customer.ID;
            mail.Date = DateTime.Now;
            mail.FilePath = filePath;

            mailRepository.SaveMail(mail);
        }
    }
}
