using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Abstract
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }
        void SaveCustomer(Customer customer);
        Customer DeleteCustomer(int customerID);
    }
}
