using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Logger;

namespace InvoiceConverter.Domain.Companies
{
    public class Katren : ConvFile
    {
        public Katren(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            string fileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".xml");
            
            MyFile myFile = new MyFile(_docXML.Customer.Number);
            newFilePath = myFile.GetFilePathWithCustNumber(Settings.folderConv, fileName);
        }

        public override void CreateAndSaveFile()
        {
            XDocument doc = XDocument.Load(filePath);
            
            var groupEl = doc.Root.Elements().Descendants("MENGE").Where(el => el.Value == "0").ToList();

            foreach (XElement element in groupEl)
            {
                XElement parent = element.Parent;
                parent.Remove();
            }
            
            doc.Save(newFilePath);

            LoggerManager.Logger.Information(_docXML.Customer.Name + " Файл {filePath} был конвертирован в файл {newfilename}", filePath, newFilePath);
            //MoveFile(Settings.folderXML);
        }
    }
}
