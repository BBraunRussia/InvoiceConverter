using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/* TXT */

namespace InvoiceConverter.Domain.Companies
{
    public class AnteyFarma : ConvFile
    {        
        public AnteyFarma(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            string fileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".txt");

            MyFile myFile = new MyFile(_docXML.Customer.Number);
            newFilePath = myFile.GetFilePathWithCustNumber(Settings.folderConv, fileName);
        }

        public override void CreateAndSaveFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(newFilePath, false, ANSI))
                {
                    sw.WriteLine(string.Concat("Антей - Фарма\t", _docXML.Invoice, "\t", _docXML.InvoiceDate));

                    for (int k = 0; k < _docXML.idTnrProductCode.Count; k++)
                    {
                        string str = string.Concat(_docXML.idTnrProductCode.GetItem(k), "\t", _docXML.kText.GetItem(k), "\t", _docXML.mfName1.GetItem(k),
                            "\t", _docXML.menge.GetItem(k), "\t", _docXML.priceNoVat.GetItem(k), "\t", _docXML.vatrate.GetItem(k), "\t",
                            _docXML.gtdNo.GetItem(k), "\t", _docXML.labelBatch.GetItem(k), "\t", _docXML.vfDat.GetItem(k), "\t",
                            _docXML.ruRegCertificate.GetItem(k), "\t", _docXML.ruIssueDateCertifacate.GetItem(k), "\t",
                            _docXML.ruValidToDateCertificate.GetItem(k), "\t", _docXML.actManPrRub.GetItem(k));

                        sw.WriteLine(str);
                    }

                    LoggerManager.Logger.Information(_docXML.Customer.Name + " Файл {filePath} был конвертирован в файл {newfilename}", filePath, newFilePath);
                    MoveFile(Settings.folderXML);
                }
            }
            catch (Exception err)
            {
                LoggerManager.Logger.Error(err, _docXML.Customer.Name + " Ошибка при обработке файла {filename}", newFilePath);
                File.Delete(newFilePath);
                MoveFile(Settings.folderXMLError);
            }
        }
    }
}
