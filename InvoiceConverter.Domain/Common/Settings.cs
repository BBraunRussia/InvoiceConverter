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
        private const string SETTINGS_FILENAME = @"settings.ini";
        private static readonly char[] SEPARATOR = new char[] { '=' };

        public static readonly string folderNew;// = @"P:\SAP\EDI\DEA\EDI_NEW";
        public static readonly string folderXML;// = @"P:\SAP\EDI\DEA\EDI_XML";
        public static readonly string folderConv;// = @"P:\SAP\EDI\DEA\EDI_CONVERTED";
        public static readonly string folderXMLError;
        public static readonly string xmlSchema;// = @"J:\Information Technology\Development\XMLtoTXT\INVOIC02_XMLSchema.xsd";
        public static readonly Dictionary<string, Cust> Customers;        

        static Settings()
        {
            Customers = new Dictionary<string, Cust>();

            string[] strArr = File.ReadAllLines(SETTINGS_FILENAME);

            foreach (var line in strArr)
            {
                string[] spliredLine = line.Split(SEPARATOR);
                switch (spliredLine[0].Trim())
                {
                    case "folderNew":
                        folderNew = spliredLine[1].Trim();
                        break;
                    case "folderXML":
                        folderXML = spliredLine[1].Trim();
                        break;
                    case "folderConv":
                        folderConv = spliredLine[1].Trim();
                        break;
                    case "folderXMLError":
                        folderXMLError = spliredLine[1].Trim();
                        break;
                    case "xmlSchema":
                        xmlSchema = spliredLine[1].Trim();
                        break;
                    case "emails":
                        Sender.AddEmails(spliredLine[1]);
                        break;
                    default:
                        ReadCustomers(line);
                        break;
                }
            }
        }

        private static void ReadCustomers(string line)
        {
            int id;
            if ((line.Count() > 0) && (int.TryParse(line[0].ToString(), out id)))
            {
                string[] splitedLine = line.Split(SEPARATOR);

                int.TryParse(splitedLine[0].Trim(), out id);

                if (id == 0)
                    return;

                Customers.Add(splitedLine[1].Split('\t')[0].Trim(), (Cust)id);
            }
        }
    }
}
