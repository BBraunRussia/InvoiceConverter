using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Formats;
using InvoiceConverter.Domain.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/* XML */

namespace InvoiceConverter.Domain.Companies
{
    public class GrandCapital : ConvFile
    {
        private const string INN = "7729418511";
        private const string OUR_COMPANY_CODE = "001127";
        private const string OUR_COMPANY_NAME = "ООО \"Б.Браун Медикал\"";

        public GrandCapital(string fileName, DocXML docXML)
            : base(fileName, docXML)
        { }

        protected override void CreateFileName()
        {
            string fileName = string.Concat("invoice_", INN, "_", _docXML.Invoice, ".xml");

            MyFile myFile = new MyFile(_docXML.Customer.Number);
            newFilePath = myFile.GetFilePathWithCustNumber(Settings.folderConv, fileName);
        }

        public override void CreateAndSaveFile()
        {
            MyXML myXML = new MyXML(newFilePath, "Document");

            myXML.CreateXMLNode("DocNumber", _docXML.Torg12);
            myXML.CreateXMLNode("DocDate", _docXML.Torg12Date);
            myXML.CreateXMLNode("SupplierCode", OUR_COMPANY_CODE);
            myXML.CreateXMLNode("SupplierName", OUR_COMPANY_NAME);
            myXML.CreateXMLNode("Sum", _docXML.sumOptWithNDS);
            myXML.CreateXMLNode("SumNDS", _docXML.sumNDS);
            
            for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
            {
                XmlNode node = myXML.CreateXMLNode("Item", string.Empty);
                myXML.CreateXMLNode("ItemCode", _docXML.idTnrProductCode.GetItem(i), node);
                myXML.CreateXMLNode("ItemName", _docXML.kText.GetItem(i), node);
                myXML.CreateXMLNode("Manuf", _docXML.mfName1.GetItem(i), node);
                myXML.CreateXMLNode("ManufCountry", _docXML.gtdHerkl.GetItem(i), node);
                myXML.CreateXMLNode("Qty", _docXML.menge.GetItem(i), node);
                myXML.CreateXMLNode("Price", _docXML.mengVat.GetItem(i), node);
                myXML.CreateXMLNode("Tax", _docXML.vatrate.GetItem(i), node);
                myXML.CreateXMLNode("PricewNDS", _docXML.priceNoVat.GetItem(i), node);
                myXML.CreateXMLNode("GTD", _docXML.gtdNo.GetItem(i), node);
                myXML.CreateXMLNode("Series", _docXML.labelBatch.GetItem(i), node);
                myXML.CreateXMLNode("ProductionDate", _docXML.hsDat.GetItem(i), node);
                myXML.CreateXMLNode("ExpirationDate", _docXML.vfDat.GetItem(i), node);
                myXML.CreateXMLNode("Barcode", _docXML.idTnrBarCode.GetItem(i), node);
            }
            myXML.Save();

            LoggerManager.Logger.Information(_docXML.Customer.Name + " Файл {filePath} был конвертирован в файл {newfilename}", filePath, newFilePath);
        }
    }
}
