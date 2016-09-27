using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Concrete
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Customer> Customers { get { return context.Customers; } }

        public void SaveCustomer(Customer customer)
        {
            if (customer.ID == 0)
            {
                context.Customers.Add(customer);
            }
            else
            {
                Customer dbEntry = context.Customers.Find(customer.ID);
                if (dbEntry != null)
                {
                    dbEntry.Number = customer.Number;
                    dbEntry.Name = customer.Name;
                    dbEntry.Subject = customer.Subject;
                    dbEntry.Recipient = customer.Recipient;
                    dbEntry.Body = customer.Body;
                    dbEntry.Contacts = customer.Contacts;
                    dbEntry.Enable = customer.Enable;
                }
            }

            context.SaveChanges();
        }

        public Customer DeleteCustomer(int customerID)
        {
            Customer dbEntry = context.Customers.Find(customerID);
            if (dbEntry != null)
            {
                context.Customers.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
