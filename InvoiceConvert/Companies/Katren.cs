using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InvoiceConverter.Companies
{
    public class Katren : ConvFile
    {
        public Katren(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            _newFileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".xml");
            CreateNewPath();
        }

        public override void CreateAndSaveFile()
        {
            XDocument doc = XDocument.Load(_fileName);
            var groupEl = doc.Root.Elements().Descendants("MENGE").Where(el => el.Value == "0").ToList();
            
            foreach (XElement element in groupEl)
            {
                XElement parent = element.Parent;
                parent.Remove();
            }
            
            doc.Save(_newFilePath);
            
            Logger.FileProcessed(_fileName, _newFilePath);
            //MoveFile(Settings.folderXML);
        }
    }
}
