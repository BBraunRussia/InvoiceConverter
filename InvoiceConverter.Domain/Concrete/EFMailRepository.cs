using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Concrete
{
    public class EFMailRepository : IMailRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Mail> Mails { get { return context.Mails; } }

        public void SaveMail(Mail mail)
        {
            if (mail.ID == 0)
            {
                context.Mails.Add(mail);
            }
            else
            {
                Mail dbEntry = context.Mails.Find(mail.ID);
                if (dbEntry != null)
                {
                    dbEntry.Customer_id = mail.Customer_id;
                    dbEntry.Date = mail.Date;
                    dbEntry.FilePath = mail.FilePath;
                }
            }

            context.SaveChanges();
        }
    }
}
