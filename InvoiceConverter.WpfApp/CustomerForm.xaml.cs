using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;
using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceConverter.WpfApp
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        private Customer customer;

        public CustomerForm(Customer customer)
        {
            InitializeComponent();

            this.customer = customer;

            stackPanel1.DataContext = customer;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            ICustomerRepository repository = CompositionRoot.Resolve<ICustomerRepository>();
            repository.SaveCustomer(customer);
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
