using InvoiceConverter.Domain.Mails;
using System.Windows;
using System.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;

namespace InvoiceConverter.WpfApp
{
    /// <summary>
    /// Interaction logic for MailsForm.xaml
    /// </summary>
    public partial class MailsForm : Window
    {
        public MailsForm()
        {
            InitializeComponent();

            MailsToShow repository = new MailsToShow();
            repository.Mails.Load();
            //IMailRepository repository = CompositionRoot.Resolve<IMailRepository>();

            mailsGrid.ItemsSource = repository.Mails.ToList();
        }
    }
}
