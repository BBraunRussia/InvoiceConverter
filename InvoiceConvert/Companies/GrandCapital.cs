using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/* XML */

namespace InvoiceConverter.Companies
{
    public class GrandCapital : ConvFile
    {
        private const string COMPANY_NAME = "GrandCapital";
        private const string INN = "7729418511";
        private const string OUR_COMPANY_CODE = "001127";
        private const string OUR_COMPANY_NAME = "ООО \"Б.Браун Медикал\"";

        public GrandCapital(string fileName, DocXML docXML)
            : base(fileName, docXML)
        { }

        protected override void CreateFileName()
        {
            _newFileName = string.Concat("invoice_", INN, "_", _docXML.Invoice, ".xml");
            CreateNewPath();
        }

        public override void CreateAndSaveFile()
        {
            XmlTextWriter textWritter = new XmlTextWriter(_newFilePath, ANSI);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("Document");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument doc = new XmlDocument();
            doc.Load(_newFilePath);

            XmlNode node = doc.CreateElement("DocNumber");
            node.InnerText = _docXML.Torg12;
            doc.DocumentElement.AppendChild(node);

            XmlNode node2 = doc.CreateElement("DocDate");
            node2.InnerText =  _docXML.Torg12Date;
            doc.DocumentElement.AppendChild(node2);

            XmlNode node3 = doc.CreateElement("SupplierCode");
            node3.InnerText = OUR_COMPANY_CODE;
            doc.DocumentElement.AppendChild(node3);

            XmlNode node4 = doc.CreateElement("SupplierName");
            node4.InnerText = OUR_COMPANY_NAME;
            doc.DocumentElement.AppendChild(node4);

            XmlNode node5 = doc.CreateElement("Sum");
            node5.InnerText = _docXML.sumOptWithNDS;
            doc.DocumentElement.AppendChild(node5);

            XmlNode node6 = doc.CreateElement("SumNDS");
            node6.InnerText = _docXML.sumNDS;
            doc.DocumentElement.AppendChild(node6);
            
            for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
            {
                XmlNode nodeItem = doc.CreateElement("Item");
                doc.DocumentElement.AppendChild(nodeItem);

                XmlNode node7 = doc.CreateElement("ItemCode");
                node7.InnerText = _docXML.idTnrProductCode.GetItem(i);
                nodeItem.AppendChild(node7);

                XmlNode node8 = doc.CreateElement("ItemName");
                node8.InnerText = _docXML.kText.GetItem(i);
                nodeItem.AppendChild(node8);

                XmlNode node9 = doc.CreateElement("Manuf");
                node9.InnerText = _docXML.mfName1.GetItem(i);
                nodeItem.AppendChild(node9);

                XmlNode node10 = doc.CreateElement("ManufCountry");
                node10.InnerText = _docXML.gtdHerkl.GetItem(i);
                nodeItem.AppendChild(node10);

                XmlNode node11 = doc.CreateElement("Qty");
                node11.InnerText = _docXML.menge.GetItem(i);
                nodeItem.AppendChild(node11);

                XmlNode node12 = doc.CreateElement("Price");
                node12.InnerText = _docXML.mengVat.GetItem(i);
                nodeItem.AppendChild(node12);

                XmlNode node13 = doc.CreateElement("Tax");
                node13.InnerText = _docXML.vatrate.GetItem(i);
                nodeItem.AppendChild(node13);

                XmlNode node14 = doc.CreateElement("PricewNDS");
                node14.InnerText = _docXML.priceNoVat.GetItem(i);
                nodeItem.AppendChild(node14);

                XmlNode node15 = doc.CreateElement("GTD");
                node15.InnerText = _docXML.gtdNo.GetItem(i);
                nodeItem.AppendChild(node15);

                XmlNode node16 = doc.CreateElement("Series");
                node16.InnerText = _docXML.labelBatch.GetItem(i);
                nodeItem.AppendChild(node16);

                XmlNode node17 = doc.CreateElement("ProductionDate");
                node17.InnerText = _docXML.hsDat.GetItemAsDate(i);
                nodeItem.AppendChild(node17);

                XmlNode node18 = doc.CreateElement("ExpirationDate");
                node18.InnerText = _docXML.vfDat.GetItem(i);
                nodeItem.AppendChild(node18);

                XmlNode node19 = doc.CreateElement("Barcode");
                node19.InnerText = _docXML.idTnrBarCode.GetItem(i);
                nodeItem.AppendChild(node19);
            }
            doc.Save(_newFilePath);

            Logger.FileProcessed(_fileName, _newFilePath);
            //MoveFile(Settings.folderXML);
        }

        private XmlNode CreateXMLNode(XmlDocument doc, string nodeName, string innerText)
        {
            XmlNode node = doc.CreateElement(nodeName);
            node.InnerText = innerText;
            return node;
        }
    }
}
