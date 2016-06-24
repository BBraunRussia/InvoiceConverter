using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Net.Mail;
using System.Data.OleDb;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {                        
            string[] filePaths = MyFile.GetFiles();
            if (filePaths == null)
                return;

            foreach (string filePath in filePaths)
            {
                Logger.BeginWork();

                DocXML docXML = new DocXML(filePath);

                FileInfo test = new FileInfo(filePath);
                string fileName = test.Name; // имя файла

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

                string newFileName = WorkWithString.CreateString(docXML.Invoice, "_", docXML.InvoiceDate);

                switch (docXML.Customer)
                {
                    case Cust.Katren:
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.Shaklin:
                            Shaklin shaklin = new Shaklin(fileName, docXML);
                            shaklin.CreateAndSaveFile();
                            move = false;
                            break;
                    case Cust.AptekaSkal:
                            GbuzOKB fileDbf = new GbuzOKB(fileName, docXML);
                            fileDbf.CreateFiles();
                            break;
                    case Cust.Protek:
                            newFileName = WorkWithString.CreateString(docXML.Invoice);
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.EuropeanMedicalCenter:
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.Optimed:
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.Voltars:
                            Voltars voltars = new Voltars(fileName, docXML);
                            voltars.CreateAndSaveFile();
                            move = false;
                            break;
                    case Cust.Optimed2:
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.UralApteka:
                            UralApteka uralApteka = new UralApteka(fileName, docXML);
                            uralApteka.CreateAndSaveFile();
                            move = false;
                            break;
                    case Cust.AnteyFarma:
                            AnteyFarma anteyFarma = new AnteyFarma(fileName, docXML);
                            anteyFarma.CreateAndSaveFile();
                            move = false;
                            break;
                    case Cust.GBUZ:
                            myFile.Copy(fileName, newFileName);
                            break;
                    case Cust.SeveroZapad:
                            SeveroZapad severoZapad = new SeveroZapad(fileName, docXML);
                            severoZapad.CreateAndSaveFile();
                            move = false;
                            break;
                    default:
                            myFile.Copy(fileName, newFileName);
                            break;
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
