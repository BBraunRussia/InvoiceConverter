using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}