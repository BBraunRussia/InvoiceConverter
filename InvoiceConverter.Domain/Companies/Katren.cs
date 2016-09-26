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
        const string COMPANY_NAME = "Katren";

        public Katren(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            LoggerManager.Logger.Debug(COMPANY_NAME + " enter in CreateFileName");
            _newFileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".xml");
            CreateNewPath();
            LoggerManager.Logger.Debug(COMPANY_NAME + " return from CreateFileName");
        }

        public override void CreateAndSaveFile()
        {
            LoggerManager.Logger.Debug(COMPANY_NAME + " load xml {file}", _fileName);
            XDocument doc = XDocument.Load(_fileName);
            LoggerManager.Logger.Debug(COMPANY_NAME + " find elements MENGE");
            var groupEl = doc.Root.Elements().Descendants("MENGE").Where(el => el.Value == "0").ToList();

            LoggerManager.Logger.Debug(COMPANY_NAME + " foreach elements");
            foreach (XElement element in groupEl)
            {
                XElement parent = element.Parent;
                parent.Remove();
            }

            LoggerManager.Logger.Debug(COMPANY_NAME + " save file");

            doc.Save(_newFilePath);

            LoggerManager.Logger.Information(COMPANY_NAME + " Файл {filename} был конвертирован в файл {newfilename}", _fileName, _newFilePath);
            //MoveFile(Settings.folderXML);
        }
    }
}
