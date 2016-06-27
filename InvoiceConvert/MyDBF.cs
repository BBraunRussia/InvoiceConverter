using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceConverter
{
    public class MyDBF
    {
        private string fileName;
        private DocXML docXML;

        public MyDBF(string fileName, DocXML docXML)
        {
            this.fileName = fileName;
            this.docXML = docXML;
        }

        public void DataTableIntoDBF(DataTable dt, string createSqlTable)
        {
            ArrayList list = new ArrayList();

            string path = string.Concat(Settings.folderConv, @"\", docXML.CustNumberSAP, @"\", fileName, ".dbf");
            string pathWithoutFile = string.Concat(Settings.folderConv, @"\", docXML.CustNumberSAP);

            if (File.Exists(path))
                File.Delete(path);

            foreach (DataColumn dc in dt.Columns)
                list.Add(dc.ColumnName);

            OleDbConnection con = new OleDbConnection(GetConnection(pathWithoutFile));
            con.Open();

            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = con;

            cmd.CommandText = createSqlTable;

            cmd.ExecuteNonQuery();

            foreach (DataRow row in dt.Rows)
            {
                string insertSql = "insert into " + fileName + " values(";

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
