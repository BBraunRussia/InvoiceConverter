using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Converter
{
    public static class Settings
    {
        public static string folderNew;// = @"P:\SAP\EDI\DEA\EDI_NEW";
        public static string folderXML;// = @"P:\SAP\EDI\DEA\EDI_XML";
        public static string folderConv;// = @"P:\SAP\EDI\DEA\EDI_CONVERTED";
        public static string folderXMLError;
        public static string xmlSchema;// = @"J:\Information Technology\Development\XMLtoTXT\INVOIC02_XMLSchema.xsd";
        public static Dictionary<Cust, string> Customers;        

        static Settings()
        {
            string line;
            string[] strArr;
            char[] charArr = new char[] { '=' };
            Customers = new Dictionary<Cust, string>();

            FileStream fs = new FileStream("settings.ini", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            try
            {
                while (sr.EndOfStream != true)
                {
                    line = sr.ReadLine();
                    strArr = line.Split(charArr);
                    switch (strArr[0].Trim())
                    {
                        case "folderNew":
                            {
                                folderNew = strArr[1].Trim();
                                break;
                            }
                        case "folderXML":
                            {
                                folderXML = strArr[1].Trim();
                                break;
                            }
                        case "folderConv":
                            {
                                folderConv = strArr[1].Trim();
                                break;
                            }
                        case "folderXMLError":
                            {
                                folderXMLError = strArr[1].Trim();
                                break;
                            }
                        case "xmlSchema":
                            {
                                xmlSchema = strArr[1].Trim();
                                break;
                            }
                        case "emails":
                            {
                                Sender.AddEmails(strArr[1]);
                                break;
                            }
                        case "#":
                            {
                                while (sr.EndOfStream != true)
                                {
                                    line = sr.ReadLine();
                                    strArr = line.Split(charArr);

                                    Cust custNumber = (Cust)Convert.ToInt32(strArr[0].Trim());

                                    Customers.Add(custNumber, strArr[1].Split('\t')[0].Trim());
                                }

                                break;
                            }
                    }
                }
            }
            catch (Exception err)
            {
                Logger.ErrorCreated("Файл настроек", err.Message);
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }
    }
}
