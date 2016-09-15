using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Serilog;

namespace InvoiceConverter.Companies
{
    public class Katren : ConvFile
    {
        const string COMPANY_NAME = "Katren";

        public static ILogger logger = LoggerManager.Logger;

        public Katren(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            logger.Debug(COMPANY_NAME + " enter in CreateFileName");
            _newFileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".xml");
            CreateNewPath();
            logger.Debug(COMPANY_NAME + " return from CreateFileName");
        }

        public override void CreateAndSaveFile()
        {
            logger.Debug(COMPANY_NAME + " load xml {file}", _fileName);
            XDocument doc = XDocument.Load(_fileName);
            logger.Debug(COMPANY_NAME + " find elements MENGE");
            var groupEl = doc.Root.Elements().Descendants("MENGE").Where(el => el.Value == "0").ToList();

            logger.Debug(COMPANY_NAME + " foreach elements");
            foreach (XElement element in groupEl)
            {
                XElement parent = element.Parent;
                parent.Remove();
            }

            logger.Debug(COMPANY_NAME + " save file");

            doc.Save(_newFilePath);

            logger.Information(COMPANY_NAME + " Файл {filename} был конвертирован в файл {newfilename}", _fileName, _newFilePath);
            //MoveFile(Settings.folderXML);
        }
    }
}
