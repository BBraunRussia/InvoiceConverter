using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Formats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/* DBF */

namespace InvoiceConverter.Domain.Companies
{
    public class AptekaSkladChel : ConvFile
    {
        private const int NUMERIC_DEFAULT = 0;

        private const string INN = "";
        private const int CONSIG = 505;
        private const int ORDER = 2;
        private const string REMARK = "";

        public AptekaSkladChel(string sourceFileName, DocXML docXML)
            : base(sourceFileName, docXML) { }

        public override void CreateAndSaveFile()
        {
            _newFileName = "H" + _docXML.Invoice;
            MyDBF myDBF = new MyDBF(_newFileName, _docXML);            
            DataTable dt = createTableHeader();
            string createSqlTable = string.Concat("create table ", _newFileName, " ([DocNumber] char(15), ", "[RegDate] Date, ", "[Inn] char(22), ",
                "[Contr] int, ", "[Consig] int, ", "[Order] int, ", "[Remark] char(100))");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
            

            _newFileName = "B" + _docXML.Invoice;
            myDBF = new MyDBF(_newFileName, _docXML);
            dt = createTableBody();
            createSqlTable = string.Concat("create table ", _newFileName, " ([DocNumber] char(15), ", "[GoodsID] char(20), ", "[GoodsN] char(100), ",
                "[CountryID] numeric(10, 0), ", "[CountryN] char(100), ", "[FirmID] numeric(10,0), ", "[FirmN] char(100), ", "[Quantity] numeric(15,3), ",
                "[PriceReg] numeric(15,2), ", "[PriceF] numeric(15,2), ", "[Margin] numeric(10,2), ", "[MarginSum] numeric(10,2), ", "[PriceWN] numeric(15,2), ",
                "[StrSumWN] numeric(15,2), ", "[Price] numeric(15,2), ", "[StrSum] numeric(15,2), ", "[NDS] numeric(10,2), ", "[NDSSum] numeric(15,2), ",
                "[N5] numeric(10,2), ", "[N5Sum] numeric(15,2), ", "[CCDNumber] char(25), ", "[BestBefore] date, ", "[DoM] date, ", "[Series] char(20), ",
                "[Analysis] char(20), ", "[AnalysisDR] date, ", "[AnalysisV] date, ", "[AnalysisID] numeric(10,0), ", "[AnalysisN] char(100), ",
                "[Analysin] char(20), ", "[AnalysinDR] date)");
            myDBF.DataTableIntoDBF(dt, createSqlTable);
        }

        private DataTable createTableHeader()
        {
            //* - обязательные поля
            DataTable dt = new DataTable();
            dt.Columns.Add("DocNumber", typeof(String));//*
            dt.Columns.Add("RegDate", typeof(DateTime));//*
            dt.Columns.Add("Inn", typeof(String));
            dt.Columns.Add("Contr", typeof(Int32));//*
            dt.Columns.Add("Consig", typeof(Int32));//*
            dt.Columns.Add("Order", typeof(Int32));//*
            dt.Columns.Add("Remark", typeof(String));

            dt.Rows.Add(new object[7] { _docXML.Torg12.Cut(15), _docXML.Torg12Date, INN, _docXML.Contr, CONSIG, ORDER, REMARK });

            return dt;
        }

        private DataTable createTableBody()
        {
            //* - обязательные поля
            DataTable dt = new DataTable();
            dt.Columns.Add("DocNumber", typeof(String));//*
            dt.Columns.Add("GoodsID", typeof(String));//*
            dt.Columns.Add("GoodsN", typeof(String));//*
            dt.Columns.Add("CountryID", typeof(Int32));
            dt.Columns.Add("CountryN", typeof(String));//*

            dt.Columns.Add("FirmID", typeof(Int32));
            dt.Columns.Add("FirmN", typeof(String));//*
            dt.Columns.Add("Quantity", typeof(Double));//*
            dt.Columns.Add("PriceReg", typeof(Double));//*0
            dt.Columns.Add("PriceF", typeof(Double));//*0

            dt.Columns.Add("Margin", typeof(Double));
            dt.Columns.Add("MarginSum", typeof(Double));
            dt.Columns.Add("PriceWN", typeof(Double));//*
            dt.Columns.Add("StrSumWN", typeof(Double));//*
            dt.Columns.Add("Price", typeof(Double));//*

            dt.Columns.Add("StrSum", typeof(Double));//*
            dt.Columns.Add("NDS", typeof(Double));//*
            dt.Columns.Add("NDSSum", typeof(Double));//*
            dt.Columns.Add("N5", typeof(Double));
            dt.Columns.Add("N5Sum", typeof(Double));

            dt.Columns.Add("CCDNumber", typeof(String));//*
            dt.Columns.Add("BestBefore", typeof(DateTime));//*
            dt.Columns.Add("DoM", typeof(DateTime));//*
            dt.Columns.Add("Series", typeof(String));//*
            dt.Columns.Add("Analysis", typeof(String));//*
            dt.Columns.Add("AnalysisDR", typeof(DateTime));
            dt.Columns.Add("AnalysisV", typeof(DateTime));//*
            dt.Columns.Add("AnalysisID", typeof(Int32));
            dt.Columns.Add("AnalysisN", typeof(String));
            dt.Columns.Add("Analysin", typeof(String));
            dt.Columns.Add("AnalysinDR", typeof(DateTime));
            
            for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
            {
                dt.Rows.Add(new object[] { 
                    _docXML.Torg12,
                    _docXML.idTnrProductCode.GetItem(i),
                    _docXML.tdLine.GetItem(i).Cut(100),
                    NUMERIC_DEFAULT,
                    _docXML.gtdHerkl.GetItem(i).Cut(100),
                    
                    NUMERIC_DEFAULT,
                    _docXML.mfName1.GetItem(i),
                    _docXML.menge.GetItem(i),
                    NUMERIC_DEFAULT,
                    _docXML.actManPrRub.GetItem(i),
                    
                    NUMERIC_DEFAULT,
                    NUMERIC_DEFAULT,
                    _docXML.priceNoVat.GetItem(i),
                    _docXML.mengNoVat.GetItem(i),
                    GetPriceForOne(_docXML.mengVat.GetItem(i), _docXML.menge.GetItem(i)),
                    
                    _docXML.mengVat.GetItem(i), //strSum
                    _docXML.vatrate.GetItem(i), //NDS
                    _docXML.totalVat.GetItem(i), //NDSSum
                    NUMERIC_DEFAULT,
                    NUMERIC_DEFAULT,

                    _docXML.gtdNo.GetItem(i),
                    _docXML.vfDat.GetItem(i),
                    _docXML.hsDat.GetItem(i),
                    _docXML.labelBatch.GetItem(i),
                    _docXML.ruRegCertificate.GetItem(i),

                    _docXML.ruIssueDateCertifacate.GetItem(i),
                    (_docXML.ruValidToDateCertificate.GetItem(i) == string.Empty) ? new DateTime(1, 1, 1) : Convert.ToDateTime(_docXML.ruValidToDateCertificate.GetItem(i)),
                    NUMERIC_DEFAULT,
                    _docXML.declCenter.GetItem(i),
                    string.Empty,
                    new DateTime(1, 1, 1) });
            }
            
            return dt;
        }

        private string GetPriceForOne(string sum, string count)
        {
            double sumD = Convert.ToDouble(sum);
            double countInt = Convert.ToDouble(count);
            return ( sumD / countInt ).ToString();
        }
    }
}
