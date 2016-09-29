using InvoiceConverter.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Domain.Abstract
{
    public abstract class ConvFile
    {
        protected static Encoding ANSI = Encoding.GetEncoding(1251);

        protected DocXML _docXML;
        protected string newFilePath;
        protected string filePath;

        public abstract void CreateAndSaveFile();

        public ConvFile(string filePath, DocXML docXML)
        {
            this.filePath = filePath;
            _docXML = docXML;

            CreateFileName();
        }

        protected virtual void CreateFileName() { }

        protected void MoveFile(string destFolder)
        {
            MyFile myFile = new MyFile(_docXML.Customer.Number);
            myFile.MoveFile(destFolder, filePath);
        }
        /*
        protected void CreateNewPath()
        {
            _newFilePath = string.Concat(Settings.folderConv, @"\", _docXML.CustNumberSAP, @"\", _newFileName);
        }
        */
    }
}
