using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Abstract
{
    public interface IMailRepository
    {
        IQueryable<Mail> Mails { get; }
        void SaveMail(Mail mail);
    }
}
