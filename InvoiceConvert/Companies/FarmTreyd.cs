using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InvoiceConverter.Companies
{
    public class FarmTreyd : ConvFile
    {
        private const int NUMERIC_DEFAULT = 0;

        public FarmTreyd(string sourceFileName, DocXML docXML)
            : base(sourceFileName, docXML) { }

        protected override void CreateFileName()
        {
            _newFileName = _docXML.Invoice;
        }

        public override void CreateAndSaveFile()
        {
            MyDBF myDBF = new MyDBF(_newFileName, _docXML);
            DataTable dt = createTable();
            string createSqlTable = string.Concat("CREATE TABLE ", _newFileName, " ([NDOC] char(20), ", "[DATEDOC] Date, ", "[CODEPST] char(12), ",
                "[EAN13] char(13), ", "[PRICE1] numeric(9,2), ", "[PRICE2] numeric(9,2), ", "[PRICE2N] numeric(9,2), ", "[QNT] numeric(9,2), ",
                "[SER] char(20), ", "[GDATE] Date, ", "[DATEMADE] Date, ", "[NAME] char(80), ", "[CNTR] char(15), ",
                "[FIRM] char(40), ", "[QNTPACK] numeric(8,0), ", "[NDS] numeric(9,2), ", "[NSP] numeric(9, 2), ", "[GNVLS] numeric(1,0), ",
                "[REGPRC] char(1), ", "[DATEPRC] char(1), ", "[NUMGTD] char(30), ", "[SERTIF] char(80), ", "[SERTDATE] char(1), ", "[SERTORG] char(80), ",//"[REGPRC] numeric(9,2),
                "[SUMPAY] numeric(9,2), ", "[BILLNUM] char(20), ", "[BILLDT] date, ", "[CNTRMADE] char(15), ", "[SERTGIVE] Date, ",
                "[SUMSNDS] numeric(12,2))");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
        }

        private DataTable createTable()
        {
            //* - обязательное поле
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
            dt.Columns.Add("SERTDATE", typeof(String));
            dt.Columns.Add("SERTORG", typeof(String));
            dt.Columns.Add("SUMPAY", typeof(Double));//*
            dt.Columns.Add("BILLNUM", typeof(String));//*
            dt.Columns.Add("BILLDT", typeof(DateTime));//*
            dt.Columns.Add("CNTRMADE", typeof(String));
            dt.Columns.Add("SERTGIVE", typeof(DateTime));
            dt.Columns.Add("SUMSNDS", typeof(Double));//*

            for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
            {
                dt.Rows.Add(new object[] { _docXML.Torg12.Cut(20), _docXML.Torg12Date, _docXML.idTnrProductCode.GetItem(i).CutFromLeft(12),
                    string.Empty, _docXML.actManPrRub.GetItem(i), _docXML.mengVat.GetItem(i), _docXML.priceNoVat.GetItem(i), _docXML.menge.GetItem(i),
                    _docXML.labelBatch.GetItem(i).Cut(20), _docXML.vfDat.GetItem(i), _docXML.hsDat.GetItem(i), _docXML.kText.GetItem(i).Cut(80),
                    _docXML.gtdHerkl.GetItem(i).Cut(15), _docXML.mfName1.GetItem(i).Cut(40), _docXML.countInPackage.GetItem(i),
                    _docXML.vatrate.GetItem(i), NUMERIC_DEFAULT, _docXML.zhnvls, NUMERIC_DEFAULT, null, _docXML.gtdNo.GetItem(i).Cut(30),
                    _docXML.ruRegCertificate.GetItem(i).Cut(80), _docXML.ruValidToDateCertificate.GetItem(i),
                    _docXML.declCenter.GetItem(i).Cut(80), _docXML.Summe, _docXML.Invoice.Cut(20), _docXML.InvoiceDate,
                    _docXML.gtdHerkl.GetItem(i).Cut(15), _docXML.ruIssueDateCertifacate.GetItem(i), _docXML.totalVat.GetItem(i) });
            }

            return dt;
        }
    }
}
