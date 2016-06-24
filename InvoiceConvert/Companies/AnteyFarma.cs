using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/* TXT */

namespace InvoiceConverter.Companies
{
    public class AnteyFarma : ConvFile
    {
        const string COMPANY_NAME = "AnteyFarma";

        public AnteyFarma(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            _newFileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".txt");
            CreateNewPath();
        }

        public override void CreateAndSaveFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_newFilePath, false, ANSI))
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

                    Logger.FileProcessed(_fileName, _newFilePath);
                    MoveFile(Settings.folderXML);
                }
            }
            catch (Exception err)
            {
                Logger.ErrorCreated(COMPANY_NAME, err.Message);
                File.Delete(_newFilePath);
                MoveFile(Settings.folderXMLError);
            }
        }
    }
}
