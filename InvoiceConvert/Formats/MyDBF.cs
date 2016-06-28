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
            this.fileName = fileName.Cut(8);
            this.docXML = docXML;
        }

        public void DataTableIntoDBF(DataTable dt, string createSqlTable)
        {
            string path = string.Concat(Settings.folderConv, @"\", docXML.CustNumberSAP, @"\", fileName, ".DBF");
            string pathWithoutFile = string.Concat(Settings.folderConv, @"\", docXML.CustNumberSAP);

            if (File.Exists(path))
                File.Delete(path);

            using (OleDbConnection connection = new OleDbConnection(CreateConnectionString(pathWithoutFile)))
            {
                connection.Open();

                //создаём таблицу
                OleDbCommand command = new OleDbCommand(createSqlTable, connection);
                command.ExecuteNonQuery();

                foreach (DataRow row in dt.Rows)
                {
                    //вставляем строки
                    command.CommandText = string.Concat("INSERT INTO ", fileName, " VALUES(", GetString(row), ")");

                    command.ExecuteNonQuery();
                }
            }
        }

        private string CreateConnectionString(string path)
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + path + "\";Extended Properties=dBASE IV;";
        }

        private string GetString(DataRow row)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in row.ItemArray)
            {
                builder.Append("'");
                builder.Append(item.ToString().ReplaceEscape());
                builder.Append("',");
            }

            return builder.ToString().Cut(builder.Length - 1);
        }
    }
}
