using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/* DBF */

namespace InvoiceConverter.Companies
{
    public class GbuzOKB : ConvFile
    {
        private const string INN = "";
        private const int CONSIG = 505;
        private const int ORDER = 0;
        private const string REMARK = "";

        public GbuzOKB(string sourceFileName, DocXML docXML)
            : base(sourceFileName, docXML) { }

        protected override void CreateFileName()
        {
            throw new NotImplementedException();
        }

        public override void CreateAndSaveFile()
        {
            _newFileName = "h" + _docXML.Invoice;
            MyDBF myDBF = new MyDBF(_newFileName, _docXML);            
            DataTable dt = createTableHeader();
            string createSqlTable = string.Concat("create table ", _fileName, " ([DocNumber] char(15), ", "[RegDate] Date, ", "[Inn] char(22), ",
                "[Contr] int, ", "[Consig] int, ", "[Order] int, ", "[Remark] char(100))");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
            

            _newFileName = "b" + _docXML.Invoice;
            myDBF = new MyDBF(_newFileName, _docXML);
            dt = createTableBody();
            createSqlTable = string.Concat("create table ", _fileName, " ([DocNumber] char(15), ", "[GoodsID] numeric(10,0), ", "[GoodsN] char(100), ",
                "[CountryID] numeric(10,0), ", "[CountryN] char(100), ", "[FirmID] numeric(10,0), ", "[FirmN] char(100), ", "[Quantity] numeric(15,3), ",
                "[PriceReg] numeric(15,2), ", "[PriceF] numeric(15,2), ", "[Margin] numeric(10,2), ", "[MarginSum] numeric(10,2), ", "[PriceWN] numeric(15,2), ",
                "[StrSumWN] numeric(15,2), ", "[Price] numeric(15,2), ", "[StrSum] numeric(15,2), ", "[NDS] numeric(10,2), ", "[NDSSum] numeric(15,2), ",
                "[N5] numeric(10,2), ", "[N5Sum] numeric(15,2), ", "[CCDNumber] char(25), ", "[BestBefore] date, ", "[DoM] date, ", "[Series] char(20), ",
                "[Analysis] char(20), ", "[AnalysisDR] date, ", "[AnalysisV] date, ", "[AnalysisID] numeric(10,0), ", "[AnalysisN] char(100), ",
                "[Analysin] char(20), ", "[AnalysinDR] date)");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
        }

        private DataTable createTableHeader()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DocNumber", typeof(String));
            dt.Columns.Add("RegDate", typeof(DateTime));
            dt.Columns.Add("Inn", typeof(String));
            dt.Columns.Add("Contr", typeof(Int32));
            dt.Columns.Add("Consig", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("Remark", typeof(String));

            dt.Rows.Add(new object[7] { _docXML.Invoice, _docXML.InvoiceDate, INN, _docXML.Contr, CONSIG, ORDER, REMARK });

            return dt;
        }

        private DataTable createTableBody()
        {    
            DataTable dt = new DataTable();
            dt.Columns.Add("DocNumber", typeof(String));//nes
            dt.Columns.Add("GoodsID", typeof(Int32));//nes
            dt.Columns.Add("GoodsN", typeof(String));//nes
            dt.Columns.Add("CountryID", typeof(Int32));
            dt.Columns.Add("CountryN", typeof(String));//nes
            dt.Columns.Add("FirmID", typeof(Int32));
            dt.Columns.Add("FirmN", typeof(String));//nes
            dt.Columns.Add("Quantity", typeof(Int32));//nes
            dt.Columns.Add("PriceReg", typeof(Int32));//0
            dt.Columns.Add("PriceF", typeof(Int32));//0
            dt.Columns.Add("Margin", typeof(Int32));
            dt.Columns.Add("MarginSum", typeof(Int32));
            dt.Columns.Add("PriceWN", typeof(Int32));//nes
            dt.Columns.Add("StrSumWN", typeof(Int32));//nes
            dt.Columns.Add("Price", typeof(Int32));//nes
            dt.Columns.Add("StrSum", typeof(Int32));//nes
            dt.Columns.Add("NDS", typeof(Int32));//nes
            dt.Columns.Add("NDSSum", typeof(Int32));//nes
            dt.Columns.Add("N5", typeof(Int32));
            dt.Columns.Add("N5Sum", typeof(Int32));
            dt.Columns.Add("CCDNumber", typeof(String));//nes
            dt.Columns.Add("BestBefore", typeof(DateTime));//nes
            dt.Columns.Add("DoM", typeof(DateTime));//nes
            dt.Columns.Add("Series", typeof(String));//nes
            dt.Columns.Add("Analysis", typeof(String));//nes
            dt.Columns.Add("AnalysisDR", typeof(DateTime));
            dt.Columns.Add("AnalysisV", typeof(DateTime));//nes
            dt.Columns.Add("AnalysisID", typeof(Int32));
            dt.Columns.Add("AnalysisN", typeof(String));
            dt.Columns.Add("Analysin", typeof(String));
            dt.Columns.Add("AnalysinDR", typeof(DateTime));

            return dt;
        }
    }
}
