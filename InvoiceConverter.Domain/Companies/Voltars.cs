using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Logger;

/* TXT */

namespace InvoiceConverter.Domain.Companies
{
    public class Voltars : ConvFile
    {
        public Voltars(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }        

        protected override void CreateFileName()
        {
            _docXML.Customer.Number = "00000498";
            while (_docXML.Invoice[0] == '0')
                _docXML.Invoice = _docXML.Invoice.Remove(0, 1);

            string fileName = string.Concat(_docXML.Customer.Number, "_", _docXML.Invoice, "_", _docXML.InvoiceDate, ".txt");

            MyFile myFile = new MyFile(_docXML.Customer.Number);
            newFilePath = myFile.GetFilePathWithCustNumber(Settings.folderConv, fileName);
        }

        public override void CreateAndSaveFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(newFilePath, false, ANSI))
                {
                    sw.WriteLine(string.Concat(_docXML.Customer.Number + "\t" + _docXML.Invoice + "\t" + _docXML.InvoiceDate + "\t" +
                        _docXML.Invoice + "\t" + _docXML.InvoiceDate));

                    for (int k = 0; k < _docXML.idTnrProductCode.Count; k++)
                    {
                        string tdline = _docXML.tdLine.GetItem(k);
                        if (tdline.Length > 120)
                            tdline = tdline.Substring(0, 120);

                        sw.WriteLine(string.Concat(_docXML.idTnrProductCode.GetItem(k) + "\t" + _docXML.tdLine.GetItem(k) + "\t" + _docXML.menge.GetItem(k) +
                            "\t" + _docXML.priceNoVat.GetItem(k) + "\t" + _docXML.vatrate.GetItem(k) + "\t" + _docXML.mengVat.GetItem(k) + "\t" +
                            _docXML.gtdNo.GetItem(k) + "\t" + _docXML.gtdHerkl.GetItem(k) + "\t" + _docXML.labelBatch.GetItem(k) + "\t" +
                            _docXML.actManPrRub.GetItem(k) + "\t" + _docXML.vfDat.GetItem(k) + "\t" + _docXML.name1.GetItem(k) + "\t" +
                            _docXML.countInPackage.GetItem(k) + "\t" + _docXML.menee.GetItem(k)));
                    }

                    LoggerManager.Logger.Information(_docXML.Customer.Name + " Файл {filePath} был конвертирован в файл {newfilename}", filePath, newFilePath);
                    MoveFile(Settings.folderXML);
                }
            }
            catch (Exception err)
            {
                LoggerManager.Logger.Error(err, _docXML.Customer.Name + " Ошибка при обработке файла {filename}", newFilePath);
                MoveFile(Settings.folderXMLError);
            }
        }
    }
}
