using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Net.Mail;
using System.Data.OleDb;
using InvoiceConverter.Companies;

namespace InvoiceConverter
{
    class Program
    {
        static void Main(string[] args)
        {                        
            string[] filePaths = MyFile.GetFiles();
            if (filePaths == null)
            {
                return;
            }

            foreach (string filePath in filePaths)
            {
                Logger.BeginWork();

                DocXML docXML = new DocXML(filePath);

                string fileName = Path.GetFileName(filePath); // имя файла

                try
                {
                    docXML.fillFields();
                }
                catch (Exception err)
                {
                    Logger.ErrorCreated(fileName, err.Message);

                    MyFile.MoveFileError(fileName);

                    continue;
                }

                MyFile myFile = new MyFile(docXML.CustNumberSAP);
                
                bool move = true;

                string newFileName = string.Concat(docXML.Invoice, "_", docXML.InvoiceDate);

                ConvFile file = null;

                switch (docXML.Customer)
                {
                    case Cust.AnteyFarma:
                        file = new AnteyFarma(fileName, docXML);
                        move = false;
                        break;
                    case Cust.AptekaSkladChel:
                        file = new AptekaSkladChel(fileName, docXML);
                        break;
                    case Cust.GrandCapital:
                        file = new GrandCapital(fileName, docXML);
                        break;
                    case Cust.FarmTreid:
                        file = new FarmTreyd(fileName, docXML);
                        break;
                    case Cust.Protek:
                        newFileName = string.Concat(docXML.Invoice);
                        myFile.Copy(fileName, newFileName);
                        break;
                    case Cust.SeveroZapad:
                        file = new SeveroZapad(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Shaklin:
                        file = new Shaklin(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Voltars:
                        file = new Voltars(fileName, docXML);
                        move = false;
                        break;
                    case Cust.UralApteka:
                        file = new UralApteka(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Katren:
                        file = new Katren(fileName, docXML);
                        break;
                    case Cust.GBUZ:
                    case Cust.Optimed:
                    case Cust.Optimed2:
                    case Cust.EuropeanMedicalCenter:
                    default:
                        myFile.Copy(fileName, newFileName);
                        break;
                }

                if (file != null)
                {
                    file.CreateAndSaveFile();
                }

                if (move)
                {
                    myFile.MoveFile(Settings.folderXML, fileName);
                }
            }

            if (filePaths.Count() > 0)
            {
                Logger.EndWork();
            }
        }
    }
}
