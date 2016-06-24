using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceConverter
{
    public abstract class ConvFile
    {
        protected static Encoding ANSI = Encoding.GetEncoding(1251);

        protected DocXML _docXML;
        protected string _newFilePath;
        protected string _fileName;
        protected string _newFileName;

        protected abstract void CreateFileName();
        public abstract void CreateAndSaveFile();

        public ConvFile(string fileName, DocXML docXML)
        {
            _fileName = fileName;
            _docXML = docXML;

            CreateFileName();
        }

        protected void MoveFile(string destFolder)
        {
            MyFile myFile = new MyFile(_docXML.CustNumberSAP);
            myFile.MoveFile(destFolder, _fileName);
        }

        protected void CreateNewPath()
        {
            _newFilePath = string.Concat(Settings.folderConv, @"\", _docXML.CustNumberSAP, @"\", _newFileName);
        }
    }
}
