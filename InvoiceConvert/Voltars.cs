﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Converter
{
    public class Voltars : ConvFile
    {
        public Voltars(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }        

        protected override void CreateFileName()
        {
            _docXML.custNumber = "00000498";
            while (_docXML.Invoice[0] == '0')
                _docXML.Invoice = _docXML.Invoice.Remove(0, 1);

            _newFileName = _docXML.custNumber + "_" + _docXML.Invoice + "_" + _docXML.InvoiceDate + ".txt";
            _newFilePath = Settings.folderConv + @"\" + _docXML.CustNumberSAP + @"\" + _newFileName;
        }

        public override void CreateAndSaveFile()
        {
            Encoding ANSI = Encoding.GetEncoding(1251);
            StreamWriter sw = new StreamWriter(_newFilePath, false, ANSI);

            try
            {
                sw.WriteLine(WorkWithString.createString(_docXML.custNumber + "\t" + _docXML.Invoice + "\t" + _docXML.InvoiceDate + "\t" +
                    _docXML.Invoice + "\t" + _docXML.InvoiceDate));

                for (int k = 0; k < _docXML.idTnrProductCode.Count; k++)
                {
                    string tdline = _docXML.tdLine.GetItem(k);
                    if (tdline.Length > 120)
                        tdline = tdline.Substring(0, 120);

                    sw.WriteLine(WorkWithString.createString(_docXML.idTnrProductCode.GetItem(k) + "\t" + _docXML.tdLine.GetItem(k) + "\t" + _docXML.menge.GetItem(k) +
                        "\t" + _docXML.priceNoVat.GetItem(k) + "\t" + _docXML.vatrate.GetItem(k) + "\t" + _docXML.mengVat.GetItem(k) + "\t" +
                        _docXML.gtdNo.GetItem(k) + "\t" + _docXML.gtdHerkl.GetItem(k) + "\t" + _docXML.labelBatch.GetItem(k) + "\t" +
                        _docXML.actManPrRub.GetItem(k) + "\t" + _docXML.vfDat.GetItem(k) + "\t" + _docXML.name1.GetItem(k) + "\t" +
                        _docXML.countInPackage.GetItem(k) + "\t" + _docXML.menee.GetItem(k)));
                }

                Logger.FileProcessed(_fileName, _newFilePath);
                MoveFile(Settings.folderXML);
            }
            catch (Exception err)
            {
                Logger.ErrorCreated("Voltars", err.Message);
                File.Delete(_fileName);
                MoveFile(Settings.folderXMLError);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
