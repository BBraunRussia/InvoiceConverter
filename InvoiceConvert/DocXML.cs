using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace InvoiceConverter
{
    public class DocXML
    {
        private static string[] eiger = { "BO", "GRM", "GL", "HLT", "A94", "DD", "23", "DAY", "J2", "DMT", "KEL", "CA", "KGM",
                                 "KMQ", "KMT", "KPA", "LTR", "ST", "MTR", "MTS", "MTK", "MTQ", "MQS", "4K", "MGM",
                                 "GP", "M1", "61", "C15", "ZMB", "MLT", "MMT", "MMK", "D87", "C16", "C22", "B98",
                                 "MON", "4O", "SET", "C45", "PAL", "ZPF", "PR", "C24", "PF", "RO", "SEC", "2J",
                                 "CMT", "D33", "PK", "BX", "CT", "FTK", "PCE", "C29" };

        private static string[] eirus = { "БТ", "Г", "Г/Л", "ГЛ", "ГМ", "ГР", "ГСМ", "ДЕН", "ДЖК", "ДМ", "К", "КА", "КГ", "КГ3",
                                 "КМ", "КПА", "л", "ЛСТ", "М", "М/С", "М2", "М3", "М3С", "МА", "МГ", "МГ3", "МГЛ",
                                 "МДЛ", "МИД", "МКТ", "МЛ", "ММ", "ММ2", "ММК", "ММС", "МНМ", "МСК", "МСЦ", "МФР",
                                 "НБР", "ННМ", "ПА", "ПАЛ", "ПАР", "ПВП", "ПОД", "РУЛ", "С", "С3С", "СМ", "ТЛ",
                                 "УП", "УП.", "УПК", "ФТ2", "ШТ", "ЭДМ" };

        private string _file;
        private string _dateInvoice;

        public ItemList idTnrProductCode; //код товара(внутренний) E1EDP19 - QUALF = 002
        public ItemList idTnrBarCode; //Штрих-код E1EDP19 - QUALF = 003
        public ItemList idTnrSerialNumber; //Серийный номер E1EDP19 - QUALF = 014
        public ItemList tdLine;
        public ItemList menge;
        public ItemList priceNoVat;
        public ItemList vatrate;
        public ItemList mengVat;
        public ItemList gtdNo;
        public ItemList gtdHerkl;
        public ItemList labelBatch;
        public ItemList actManPrRub;
        public ItemList vfDat;
        public ItemList name1;
        public ItemList countInPackage;
        public ItemList menee;
        public ItemList kText;
        public ItemList mfName1;
        public ItemList mengNoVat;
        public ItemList ruRegCertificate;
        public ItemList ruIssueDateCertifacate;
        public ItemList ruValidToDateCertificate;
        public ItemList declCenter;
        public ItemList totalVat;
        public ItemList hsDat;

        public string Comp { get; set; }
        public string post; //поставщик E1EDKA1-PARVW = BK
        public string pokup; //покупатель E1EDKA1-PARVW = AG
        public string countPos; //кол-во позиций E1EDS01-SUMID = 001
        public string zhnvls;
        public string sumOpt; //суммаОпт E1EDS01-SUMID = 010
        public string sumOptWithNDS; //суммаОптВклНДС E1EDS01-SUMID = 012
        public string sumNDS; //суммаНДС E1EDS01-SUMID = 005
        public string gruzopoluchatel; //Грузополучатель E1EDPA1-PARVW = WE
        public string address; //адрес E1EDKA1-PARVW = BK
        public string phone; //телефон E1EDKA1-PARVW = BK
        public string account; //рассчётный счёт E1EDK28-BCOUN = RU
        public string city; //город E1EDK28-BCOUN = RU
        public string bank; //банк E1EDK28-BCOUN = RU
        public string otdel; //ОтделениеБанка E1EDK28-BCOUN = RU
        
        private string _torg12Date;

        public string Invoice { get; set; }
        public string CustNumber { get; set; }

        public string InvoiceDate
        {
            get { return string.Concat(_dateInvoice.Substring(6, 2), ".", _dateInvoice.Substring(4, 2), ".", _dateInvoice.Substring(0, 4)); }
            set { _dateInvoice = value; }
        }

        public string CustNumberSAP { get; private set; }
        public Cust Customer { get; private set; }
        public string Curcy { get; private set; }
        
        public string Torg12 { get; private set; }
        public string Torg12Date
        {
            get { return MyDate.GetDate(_torg12Date); }
            private set { _torg12Date = value; }
        }

        public string Summe { get; private set; }

        public int Contr { get { return ((Comp == "299") || ((Comp == "0299"))) ? 150 : 7448; } } // выбор компании Гематек или ББраун

        public DocXML(string file)
        {
            _file = file;

            idTnrProductCode = new ItemList();
            idTnrBarCode = new ItemList();
            idTnrSerialNumber = new ItemList();

            tdLine = new ItemList();
            menge = new ItemList();
            priceNoVat = new ItemList();
            vatrate = new ItemList();
            mengVat = new ItemList();
            gtdNo = new ItemList();
            gtdHerkl = new ItemList();
            labelBatch = new ItemList();
            actManPrRub = new ItemList();
            vfDat = new ItemList();
            name1 = new ItemList();
            countInPackage = new ItemList();
            menee = new ItemList();
            kText = new ItemList();
            mfName1 = new ItemList();
            mengNoVat = new ItemList();
            ruRegCertificate = new ItemList();
            ruIssueDateCertifacate = new ItemList();
            ruValidToDateCertificate = new ItemList();
            declCenter = new ItemList();
            totalVat = new ItemList();
            hsDat = new ItemList();
            
            zhnvls = "0";
        }

        public void fillFields()
        {
            DataSet ds = new DataSet();

            ds.ReadXmlSchema(Settings.xmlSchema);
            ds.ReadXml(_file, XmlReadMode.ReadSchema);

            Curcy = ds.Tables[2].Rows[0].ItemArray[3].ToString();            
            Summe = ds.Tables[29].Rows[0].ItemArray[2].ToString();

            SetCustNumberSAP(ds.Tables[1].Rows[0].ItemArray[27].ToString());

            Invoice = ds.Tables[4].Rows[0].ItemArray[2].ToString(); //номер счёт-фактуры E1EDK02-QUALF = 009
            InvoiceDate = ds.Tables[4].Rows[0].ItemArray[4].ToString(); //дата счёт-фактуры E1EDK02-QUALF = 009

            Torg12 = ds.Tables[4].Rows[2].ItemArray[2].ToString(); //номер накладной E1EDK02-QUALF = 012
            Torg12Date = ds.Tables[4].Rows[2].ItemArray[4].ToString(); //дата накладной E1EDK02-QUALF = 012
            
            Comp = ds.Tables[3].Rows[4].ItemArray[3].ToString(); //Код поставщика
            post = ds.Tables[3].Rows[4].ItemArray[4].ToString(); //поставщик E1EDKA1-PARVW = BK
            pokup = ds.Tables[3].Rows[1].ItemArray[4].ToString(); //покупатель E1EDKA1-PARVW = AG
            countPos = ds.Tables[29].Rows[0].ItemArray[1].ToString(); //кол-во позиций E1EDS01-SUMID = 001
            sumOpt = ds.Tables[29].Rows[4].ItemArray[2].ToString(); //суммаОпт E1EDS01-SUMID = 010
            sumOptWithNDS = ds.Tables[29].Rows[1].ItemArray[2].ToString(); //суммаОптВклНДС E1EDS01-SUMID = 012
            sumNDS = ds.Tables[29].Rows[2].ItemArray[2].ToString(); //суммаНДС E1EDS01-SUMID = 005
            gruzopoluchatel = ds.Tables[21].Rows[0].ItemArray[4].ToString(); //Грузополучатель E1EDPA1-PARVW = WE
            address = ds.Tables[3].Rows[4].ItemArray[8].ToString(); //адрес E1EDKA1-PARVW = BK
            phone = ds.Tables[3].Rows[4].ItemArray[19].ToString(); //телефон E1EDKA1-PARVW = BK
            account = ds.Tables[11].Rows[0].ItemArray[5].ToString(); //рассчётный счёт E1EDK28-BCOUN = RU
            city = ds.Tables[11].Rows[0].ItemArray[4].ToString(); //город E1EDK28-BCOUN = RU
            bank = ds.Tables[11].Rows[0].ItemArray[3].ToString(); //банк E1EDK28-BCOUN = RU
            otdel = ds.Tables[11].Rows[0].ItemArray[2].ToString(); //ОтделениеБанка E1EDK28-BCOUN = RU

            foreach (DataRow row in ds.Tables[19].Rows)
            {
                if (row.ItemArray[1].ToString() == "002")
                {
                    idTnrProductCode.Add(row.ItemArray[2].ToString());
                    kText.Add(WorkWithString.ReplaceSimbol(row.ItemArray[3].ToString()));
                }
                if (row.ItemArray[1].ToString() == "003")
                    idTnrBarCode.Add(row.ItemArray[2].ToString());
                if (row.ItemArray[1].ToString() == "014")
                    idTnrSerialNumber.Add(row.ItemArray[2].ToString());
            }

            foreach (DataRow row in ds.Tables[16].Rows)
            {
                menge.Add(row.ItemArray[5].ToString().Trim().Replace(".", ","));

                for (int k = 0; k < eiger.Count(); k++)
                {
                    if (eiger[k] == row.ItemArray[6].ToString().Trim())
                    {
                        menee.Add(eirus[k]);
                        break;
                    }
                }
            }

            var doc = new XmlDocument();
            doc.Load(_file);

            foreach (XmlNode table in doc.DocumentElement.ChildNodes[0].ChildNodes)
            {
                if (table.Name == "E1EDP01")
                {
                    foreach (XmlNode table2 in table.ChildNodes)
                    {
                        if (table2.Name == "E1EDPT1")
                        {
                            bool add = false;

                            foreach (XmlNode table3 in table2.ChildNodes)
                            {
                                if ((table3.Name == "E1EDPT2") && (table2.ChildNodes[1].InnerText == "R") && (table2.ChildNodes[0].InnerText == "0001"))
                                {
                                    if (add)
                                    {
                                        if (table3.ChildNodes[0].InnerText != "")
                                            tdLine.ConcValue(tdLine.Count - 1, table3.ChildNodes[0].InnerText);
                                    }
                                    else
                                    {
                                        tdLine.Add(table3.ChildNodes[0].InnerText);
                                        add = true;
                                    }
                                }   
                            }
                        }
                        if (table2.Name == "Z1EDP01_01")
                        {
                            if (((table2.ChildNodes.Count > 11) && (table2.ChildNodes[11].Name == "MVGR4") && (table2.ChildNodes[11].InnerText.Trim() == "Z01"))
                                || ((table2.ChildNodes.Count > 21) && (table2.ChildNodes[21].Name == "MVGR4") && (table2.ChildNodes[21].InnerText.Trim() == "Z01")))
                                zhnvls = "1";
                        }
                    }
                }
            }

            var nodes = doc.GetElementsByTagName("SMENG");
            int j = 0;

            foreach (XmlNode node in nodes)
            {
                double smeng = 0;
                double.TryParse(node.InnerText.Trim().Replace(".", ","), out smeng);
                if (smeng == 0)
                {
                    j++;
                    continue;
                }
                countInPackage.Add((smeng / Convert.ToDouble(menge.GetItem(j))).ToString());

                j++;
            }

            nodes = doc.GetElementsByTagName("PRICE_NOVAT");
            foreach (XmlNode node in nodes)
                priceNoVat.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("MENG_NOVAT");
            foreach (XmlNode node in nodes)
                mengNoVat.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("VATRATE");
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText.Trim() != "1-")
                    vatrate.Add((Convert.ToDouble(node.InnerText.Trim().Replace("-", "").Replace(".", ",")) * 100).ToString());
                else
                    vatrate.Add("0");
            }

            nodes = doc.GetElementsByTagName("MENG_VAT");
            foreach (XmlNode node in nodes)
                mengVat.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("GTD_NO");
            foreach (XmlNode node in nodes)
                gtdNo.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("GTD_HERKL");
            foreach (XmlNode node in nodes)
                gtdHerkl.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("TOTAL_VAT");
            foreach (XmlNode node in nodes)
                totalVat.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("MF_NAME1");
            foreach (XmlNode node in nodes)
                mfName1.Add(WorkWithString.ReplaceSimbol(node.InnerText.Trim()));

            nodes = doc.GetElementsByTagName("LABEL_BATCH");
            foreach (XmlNode node in nodes)
                labelBatch.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("ACT_MAN_PR_RUB");
            foreach (XmlNode node in nodes)
                actManPrRub.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("VFDAT");
            foreach (XmlNode node in nodes)
                vfDat.Add(node.InnerText.Trim().Substring(6, 2) + "." + node.InnerText.Trim().Substring(4, 2) + "." + node.InnerText.Trim().Substring(0, 4));            

            nodes = doc.GetElementsByTagName("RU_REG_CERTIFICATE");
            foreach (XmlNode node in nodes)
                ruRegCertificate.Add(WorkWithString.ReplaceSimbol(node.InnerText.Trim()));

            nodes = doc.GetElementsByTagName("RU_ISSUE_DATE_CERTIFICATE");
            foreach (XmlNode node in nodes)
                ruIssueDateCertifacate.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("RU_VALID_TO_DATE_CERTIFICATE");
            foreach (XmlNode node in nodes)
                ruValidToDateCertificate.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("DECL_CENTER");
            foreach (XmlNode node in nodes)
                declCenter.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("NAME1");
            foreach (XmlNode node in nodes)
                name1.Add(node.InnerText.Trim());

            nodes = doc.GetElementsByTagName("HSDAT");
            foreach (XmlNode node in nodes)
                hsDat.Add(node.InnerText.Trim());
        }
        
        public void SetCustNumberSAP(string value)
        {
            CustNumberSAP = value;

            if (Settings.Customers.ContainsKey(value))
            {
                Customer = Settings.Customers[value];
            }
            
            CreateFolder();
        }

        private void CreateFolder()
        {
            string filePath = string.Concat(Settings.folderConv, @"\", CustNumberSAP, @"\file.xml");
            MyFile.CreateFolder(filePath);
        }
    }
}
