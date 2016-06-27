using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Companies
{
    public class FarmTreid : ConvFile
    {
        public FarmTreid(string sourceFileName, DocXML docXML)
            : base(sourceFileName, docXML) { }

        public override void CreateAndSaveFile()
        {
            _newFileName = _docXML.Invoice;
            MyDBF myDBF = new MyDBF(_newFileName, _docXML);
            DataTable dt = createTable();
            string createSqlTable = string.Concat("CREATE TABLE ", _fileName, " ([NDOC] char(20), ", "[DATEDOC] Date, ", "[CODEPST] char(12), ",
                "[EAN13] char(13), ", "[PRICE1] numeric(9,2), ", "[PRICE2] numeric(9,2), ", "[PRICE2N] numeric(9,2), ", "[QNT] numeric(9,2), ",
                "[SER] char(20), ", "[GDATE] Date, ", "[DATEMADE] Date, ", "[NAME] char(80), ", "[CNTR] char(15), ",
                "[FIRM] char(40), ", "[QNTPACK] numeric(8,0), ", "[NDS] numeric(9,2), ", "[NSP] numeric(9,2), ", "[GNVLS] numeric(1,0), ",
                "[REGPRC] numeric(9,2), ", "[DATEPRC] Date, ", "[NUMGTD] char(30), ", "[SERTIF] char(80), ", "[SERTDATE] date, ", "[SERTORG] char(80), ",
                "[SUMPAY] numeric(9,2), ", "[BILLNUM] char(20), ", "[BILLDT] date, ", "[CNTRMADE] char(15), ", "[SERTGIVE] Date, ",
                "[SUMSNDS] numeric(12,2), ");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NDOC", typeof(String));//*
            dt.Columns.Add("DATEDOC", typeof(DateTime));//*
            dt.Columns.Add("CODEPST", typeof(String));//*
            dt.Columns.Add("EAN13", typeof(String));//*
            dt.Columns.Add("PRICE1", typeof(Double));//*
            dt.Columns.Add("PRICE2", typeof(Double));
            dt.Columns.Add("PRICE2N", typeof(Double));//*
            dt.Columns.Add("QNT", typeof(Double));//*
            dt.Columns.Add("SER", typeof(String));//*
            dt.Columns.Add("GDATE", typeof(DateTime));//*
            dt.Columns.Add("DATEMADE", typeof(DateTime));
            dt.Columns.Add("NAME", typeof(String));//*
            dt.Columns.Add("CNTR", typeof(String));//*
            dt.Columns.Add("FIRM", typeof(String));//*
            dt.Columns.Add("QNTPACK", typeof(Int32));
            dt.Columns.Add("NDS", typeof(Double));//*
            dt.Columns.Add("NSP", typeof(Double));
            dt.Columns.Add("GNVLS", typeof(Int32));//*
            dt.Columns.Add("REGPRC", typeof(Double));//*
            dt.Columns.Add("DATEPRC", typeof(DateTime));
            dt.Columns.Add("NUMGTD", typeof(String));//*
            dt.Columns.Add("SERTIF", typeof(String));
            dt.Columns.Add("SERTDATE", typeof(DateTime));
            dt.Columns.Add("SERTORG", typeof(String));
            dt.Columns.Add("SUMPAY", typeof(Double));//*
            dt.Columns.Add("BILLNUM", typeof(String));//*
            dt.Columns.Add("BILLDT", typeof(DateTime));//*
            dt.Columns.Add("CNTRMADE", typeof(String));
            dt.Columns.Add("SERTGIVE", typeof(DateTime));
            dt.Columns.Add("SUMSNDS", typeof(Double));//*

            for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
            {
                dt.Rows.Add(new object[] { _docXML.Torg12, _docXML.Torg12Date, _docXML.idTnrProductCode.GetItem(i), string.Empty,
                    _docXML.actManPrRub.GetItem(i), _docXML.mengVat.GetItem(i), _docXML.priceNoVat.GetItem(i), _docXML.menge.GetItem(i),
                    _docXML.labelBatch.GetItem(i), _docXML.vfDat.GetItemAsDate(i), _docXML.hsDat.GetItemAsDate(i), _docXML.kText.GetItem(i),
                    _docXML.gtdHerkl.GetItem(i), _docXML.mfName1.GetItem(i), _docXML.countInPackage.GetItem(i), _docXML.vatrate.GetItem(i),
                    string.Empty, _docXML.zhnvls, string.Empty, string.Empty, _docXML.gtdNo.GetItem(i), _docXML.ruRegCertificate.GetItem(i),
                    _docXML.ruValidToDateCertificate.GetItem(i), _docXML.declCenter.GetItem(i), _docXML.Summe, _docXML.Invoice, _docXML.InvoiceDate,
                    _docXML.gtdHerkl.GetItem(i), _docXML.ruIssueDateCertifacate.GetItem(i), _docXML.totalVat.GetItem(i) });
            }

            return dt;
        }
    }
}
