using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Infractructure;
using InvoiceConverter.Domain.Mails;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Common
{
    public static class Settings
    {
        public static readonly string folderNew;// = @"P:\SAP\EDI\DEA\EDI_NEW";
        public static readonly string folderXML;// = @"P:\SAP\EDI\DEA\EDI_XML";
        public static readonly string folderConv;// = @"P:\SAP\EDI\DEA\EDI_CONVERTED";
        public static readonly string folderXMLError;
        public static readonly string folderSent;
        public static readonly string xmlSchema;// = @"J:\Information Technology\Development\XMLtoTXT\INVOIC02_XMLSchema.xsd";
        public static readonly string adminEmail;
        //public static readonly Dictionary<string, Cust> Customers;

        static Settings()
        {
            //Customers = new Dictionary<string, Cust>();

            ISettingRepository repository = CompositionRoot.Resolve<ISettingRepository>();

            folderNew = repository.Settings.FirstOrDefault(s => s.ID == "folderNew").Value;
            folderXML = repository.Settings.FirstOrDefault(s => s.ID == "folderXML").Value;
            folderConv = repository.Settings.FirstOrDefault(s => s.ID == "folderConv").Value;
            folderXMLError = repository.Settings.FirstOrDefault(s => s.ID == "folderXMLError").Value;
            folderSent = repository.Settings.FirstOrDefault(s => s.ID == "folderSent").Value;
            xmlSchema = repository.Settings.FirstOrDefault(s => s.ID == "xmlSchema").Value;
            adminEmail = repository.Settings.FirstOrDefault(s => s.ID == "email").Value;

            //ReadCustomers();
        }
        /*
        private static void ReadCustomers()
        {
            ICustomerRepository repository = new EFCustomerRepository();

            foreach (var item in repository.Customers)
            {
                Customers.Add(item.Name, (Cust)item.ID);
            }
        }
        */
    }
}
