using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

/* DBF */

namespace InvoiceConverter.Companies
{
    public class GbuzOKB
    {
        private const string INN = "";
        private const int CONSIG = 505;
        private const int ORDER = 0;
        private const string REMARK = "";

        private DocXML _docXML;
        private string _sqlTable;
        private string _fileName;
        private string _sourceFileName;

        public GbuzOKB(string sourceFileName, DocXML docXML)
        {
            _sourceFileName = sourceFileName;
            _docXML = docXML;
        }

        public void CreateFiles()
        {
            _fileName = "h" + _docXML.Invoice;
            DataTable dt = createTableHeader();                        
            DataSetIntoDBF(dt);

            _fileName = "b" + _docXML.Invoice;
            dt = createTableBody();
            DataSetIntoDBF(dt);
        }

        private DataTable createTableHeader()
        {
            _sqlTable = string.Concat("create table ", _fileName, " ([DocNumber] char(15), ", "[RegDate] Date, ", "[Inn] char(22), ",
                "[Contr] int, ", "[Consig] int, ", "[Order] int, ", "[Remark] char(100))");

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
            _sqlTable = string.Concat("create table ", _fileName, " ([DocNumber] char(15), ", "[GoodsID] numeric(10,0), ", "[GoodsN] char(100), ",
                "[CountryID] numeric(10,0), ", "[CountryN] char(100), ", "[FirmID] numeric(10,0), ", "[FirmN] char(100), ", "[Quantity] numeric(15,3), ",
                "[PriceReg] numeric(15,2), ", "[PriceF] numeric(15,2), ", "[Margin] numeric(10,2), ", "[MarginSum] numeric(10,2), ", "[PriceWN] numeric(15,2), ",
                "[StrSumWN] numeric(15,2), ", "[Price] numeric(15,2), ", "[StrSum] numeric(15,2), ", "[NDS] numeric(10,2), ", "[NDSSum] numeric(15,2), ",
                "[N5] numeric(10,2), ", "[N5Sum] numeric(15,2), ", "[CCDNumber] char(25), ", "[BestBefore] date, ", "[DoM] date, ", "[Series] char(20), ",
                "[Analysis] char(20), ", "[AnalysisDR] date, ", "[AnalysisV] date, ", "[AnalysisID] numeric(10,0), ", "[AnalysisN] char(100), ",
                "[Analysin] char(20), ", "[AnalysinDR] date)");

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
            /*
            for (int i = 0; i < _docXML.idTnr.Count; i++)
            {
                dt.Rows.Add(new object[31] { _docXML.invoiceNumber, _docXML.idTnr.GetItem(i, 10), _docXML.tdLine.GetItem(i, 100), "", _docXML.gtdHerkl.GetItem(i),
                    "", _docXML.mfName1.GetItem(i), _docXML.menge.GetItem(i), 
                });
            }
            */

            return dt;
        }

        private void DataSetIntoDBF(DataTable dt)
        {
            ArrayList list = new ArrayList();

            string path = string.Concat(Settings.folderConv, @"\", _docXML.CustNumberSAP, @"\", _fileName, ".dbf");
            string pathWithoutFile = string.Concat(Settings.folderConv, @"\", _docXML.CustNumberSAP);

            if (File.Exists(path))
                File.Delete(path);

            string createSql = _sqlTable;            

            foreach (DataColumn dc in dt.Columns)
                list.Add(dc.ColumnName);
                        
            OleDbConnection con = new OleDbConnection(GetConnection(pathWithoutFile));
            con.Open();

            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = con;

            cmd.CommandText = createSql;

            cmd.ExecuteNonQuery();

            foreach (DataRow row in dt.Rows)
            {
                string insertSql = "insert into " + _fileName + " values(";

                for (int i = 0; i < list.Count; i++)
                {
                    insertSql = insertSql + "'" + ReplaceEscape(row[list[i].ToString()].ToString()) + "',";
                }

                insertSql = insertSql.Substring(0, insertSql.Length - 1) + ")";

                cmd.CommandText = insertSql;

                cmd.ExecuteNonQuery();
            }

            con.Close();
        }

        private string GetConnection(string path)
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + path + "\";Extended Properties=dBASE IV;";
        }

        private string ReplaceEscape(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }
    }
}
