using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Concrete;
using System.Windows;
using System.Data.Entity;
using InvoiceConverter.Domain.Entities;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace InvoiceConverter.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private EFDbContext repository;
        private ICustomerRepository repository;

        public MainWindow()
        {
            InitializeComponent();

            repository = new EFCustomerRepository();

            Load();
        }

        private void Load()
        {
            repository.Customers.Load();
            customersGrid.ItemsSource = repository.Customers.ToList();
        }

        private void customersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;
            
            Customer customer = customersGrid.SelectedItems[0] as Customer;
            if (customer != null)
            {
                openCustomer(customer);
            }
        }

        private void addNew_Click(object sender, RoutedEventArgs e)
        {
            openCustomer(new Customer());
            Load();
        }

        private void openCustomer(Customer customer)
        {
            CustomerForm custForm = new CustomerForm(customer);
            custForm.ShowDialog();
        }
    }
}
