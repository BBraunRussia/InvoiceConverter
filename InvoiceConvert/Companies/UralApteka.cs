using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/* XML */

namespace InvoiceConverter.Companies
{
    public class UralApteka : ConvFile
    {
        public UralApteka(string sourceFile, DocXML docXML)
            : base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            _newFileName = string.Concat(_docXML.Invoice, "_", _docXML.InvoiceDate, ".xml");
            CreateNewPath();
        }

        public override void CreateAndSaveFile()
        {
            XmlTextWriter textWritter = new XmlTextWriter(_newFilePath, ANSI);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("Документ");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument doc = new XmlDocument();
            doc.Load(_newFilePath);

            XmlNode node = doc.CreateElement("ЗаголовокДокумента");
            doc.DocumentElement.AppendChild(node);

            XmlNode node2 = doc.CreateElement("НомерДок");
            node2.InnerText = _docXML.Invoice;
            node.AppendChild(node2);
            XmlNode node3 = doc.CreateElement("ДатаДок");
            node3.InnerText = _docXML.InvoiceDate.Substring(6, 2) + "." + _docXML.InvoiceDate.Substring(4, 2) + "." + _docXML.InvoiceDate.Substring(0, 4);
            node.AppendChild(node3);
            XmlNode node4 = doc.CreateElement("Поставщик");
            node4.InnerText = WorkWithString.ReplaceSimbol(_docXML.post);
            node.AppendChild(node4);
            XmlNode node5 = doc.CreateElement("Получатель");
            node5.InnerText = WorkWithString.ReplaceSimbol(_docXML.pokup);
            node.AppendChild(node5);
            XmlNode node6 = doc.CreateElement("Позиций");
            node6.InnerText = _docXML.countPos;
            node.AppendChild(node6);
            XmlNode node7 = doc.CreateElement("ЖНВЛС");
            node7.InnerText = _docXML.zhnvls;
            node.AppendChild(node7);
            XmlNode node8 = doc.CreateElement("СуммаОпт");
            node8.InnerText = _docXML.sumOpt;
            node.AppendChild(node8);
            XmlNode node9 = doc.CreateElement("СуммаОптВклНДС");
            node9.InnerText = _docXML.sumOptWithNDS;
            node.AppendChild(node9);
            XmlNode node10 = doc.CreateElement("ТипДок");
            node10.InnerText = "ПРХ";
            node.AppendChild(node10);
            XmlNode node11 = doc.CreateElement("ДатаОтгрузки");
            node11.InnerText = _docXML.InvoiceDate;
            node.AppendChild(node11);
            XmlNode node12 = doc.CreateElement("СуммаНДС");
            node12.InnerText = _docXML.sumNDS;
            node.AppendChild(node12);
            XmlNode node13 = doc.CreateElement("Грузополучатель");
            node13.InnerText = WorkWithString.ReplaceSimbol(_docXML.gruzopoluchatel);
            node.AppendChild(node13);
            XmlNode node14 = doc.CreateElement("УсловияОплаты");
            node.AppendChild(node14);
            XmlNode node15 = doc.CreateElement("ТоварнаяГруппа");
            node.AppendChild(node15);
            XmlNode node16 = doc.CreateElement("Примечание");
            node.AppendChild(node16);

            XmlNode node17 = doc.CreateElement("РеквизитыПоставщика");
            doc.DocumentElement.AppendChild(node17);
            XmlNode node18 = doc.CreateElement("Адрес");
            node18.InnerText = WorkWithString.ReplaceSimbol(_docXML.address);
            node17.AppendChild(node18);
            XmlNode node19 = doc.CreateElement("ИНН");
            node17.AppendChild(node19);
            XmlNode node20 = doc.CreateElement("КПП");
            node17.AppendChild(node20);
            XmlNode node21 = doc.CreateElement("Телефоны");
            node21.InnerText = _docXML.phone;
            node17.AppendChild(node21);
            XmlNode node22 = doc.CreateElement("ОКОНХ");
            node17.AppendChild(node22);
            XmlNode node23 = doc.CreateElement("ОКПО");
            node17.AppendChild(node23);
            XmlNode node24 = doc.CreateElement("РасчетныйСчет");
            node24.InnerText = _docXML.account;
            node17.AppendChild(node24);
            XmlNode node25 = doc.CreateElement("Город");
            node25.InnerText = WorkWithString.ReplaceSimbol(_docXML.city);
            node17.AppendChild(node25);
            XmlNode node26 = doc.CreateElement("Банк");
            node26.InnerText = WorkWithString.ReplaceSimbol(_docXML.bank);
            node17.AppendChild(node26);
            XmlNode node27 = doc.CreateElement("ОтделениеБанка");
            node27.InnerText = _docXML.otdel;
            node17.AppendChild(node27);
            XmlNode node28 = doc.CreateElement("КорСчет");
            node17.AppendChild(node28);
            XmlNode node29 = doc.CreateElement("ЭлПочта");
            node17.AppendChild(node29);

            XmlNode node30 = doc.CreateElement("ТоварныеПозиции");
            doc.DocumentElement.AppendChild(node30);

            for (int k = 0; k < _docXML.idTnrProductCode.Count; k++)
            {
                XmlNode node31 = doc.CreateElement("ТоварнаяПозиция");
                node30.AppendChild(node31);
                XmlNode node32 = doc.CreateElement("Товар");
                node32.InnerText = _docXML.kText.GetItem(k);
                node31.AppendChild(node32);
                XmlNode node33 = doc.CreateElement("Изготовитель");
                node33.InnerText = _docXML.mfName1.GetItem(k);
                node31.AppendChild(node33);
                XmlNode node34 = doc.CreateElement("Количество");
                if (_docXML.menge.Count > k)
                    node34.InnerText = (Convert.ToInt32(Convert.ToDouble(_docXML.menge.GetItem(k)))).ToString();
                node31.AppendChild(node34);
                XmlNode node35 = doc.CreateElement("Количество");
                node35.InnerText = "1";
                node31.AppendChild(node35);
                XmlNode node36 = doc.CreateElement("ЦенаИзг");
                node36.InnerText = _docXML.actManPrRub.GetItem(k);
                node31.AppendChild(node36);
                XmlNode node37 = doc.CreateElement("ЦенаГР");
                node31.AppendChild(node37);
                XmlNode node38 = doc.CreateElement("НаценОпт");
                node31.AppendChild(node38);
                XmlNode node39 = doc.CreateElement("ЦенаОпт");
                node39.InnerText = _docXML.priceNoVat.GetItem(k);
                node31.AppendChild(node39);
                XmlNode node40 = doc.CreateElement("СуммаОпт");
                node40.InnerText = _docXML.mengNoVat.GetItem(k);
                node31.AppendChild(node40);
                XmlNode node41 = doc.CreateElement("СтавкаНДС");
                node41.InnerText = _docXML.vatrate.GetItem(k);
                node31.AppendChild(node41);
                XmlNode node42 = doc.CreateElement("СуммаОптВклНДС");
                node42.InnerText = _docXML.mengVat.GetItem(k);
                node31.AppendChild(node42);
                XmlNode node43 = doc.CreateElement("КодТовара");
                node43.InnerText = _docXML.idTnrProductCode.GetItem(k);
                node31.AppendChild(node43);
                XmlNode node44 = doc.CreateElement("СтранаИзготовителя");
                node44.InnerText = _docXML.gtdHerkl.GetItem(k);
                node31.AppendChild(node44);
                XmlNode node45 = doc.CreateElement("СуммаНДС");
                node45.InnerText = _docXML.totalVat.GetItem(k);
                node31.AppendChild(node45);
                XmlNode node46 = doc.CreateElement("ЦенаРозн");
                node31.AppendChild(node46);
                XmlNode node47 = doc.CreateElement("ГТД");
                node47.InnerText = _docXML.gtdNo.GetItem(k);
                node31.AppendChild(node47);
                XmlNode node48 = doc.CreateElement("Сильнодействующий");
                node31.AppendChild(node48);
                XmlNode node49 = doc.CreateElement("Рецептурный");
                node31.AppendChild(node49);
                XmlNode node50 = doc.CreateElement("ПризнакРаспак");
                node31.AppendChild(node50);
                XmlNode node51 = doc.CreateElement("КодТовара");
                node51.InnerText = _docXML.idTnrProductCode.GetItem(k);
                node31.AppendChild(node51);

                XmlNode node52 = doc.CreateElement("Серии");
                node31.AppendChild(node52);
                XmlNode node53 = doc.CreateElement("Серия");
                node52.AppendChild(node53);

                XmlNode node54 = doc.CreateElement("СерияТовара");
                node54.InnerText = _docXML.labelBatch.GetItem(k);
                node53.AppendChild(node54);
                XmlNode node55 = doc.CreateElement("СрокГодностиТовара");
                node55.InnerText = _docXML.vfDat.GetItem(k);
                node53.AppendChild(node55);
                XmlNode node56 = doc.CreateElement("НомерСертиф");
                node56.InnerText = _docXML.ruRegCertificate.GetItem(k);
                node53.AppendChild(node56);
                XmlNode node57 = doc.CreateElement("ОрганСертиф");
                node53.AppendChild(node57);
                XmlNode node58 = doc.CreateElement("ДатаВыдачиСертиф");
                node58.InnerText = _docXML.ruIssueDateCertifacate.GetItem(k);
                node53.AppendChild(node58);
                XmlNode node59 = doc.CreateElement("СрокДействияСертиф");
                node53.AppendChild(node59);
                XmlNode node60 = doc.CreateElement("РегНомер");
                node53.AppendChild(node60);
                XmlNode node61 = doc.CreateElement("РегДатаСертиф");
                node53.AppendChild(node61);
                XmlNode node62 = doc.CreateElement("РегОрганСертиф");
                node53.AppendChild(node62);
                XmlNode node63 = doc.CreateElement("Валюта");
                node63.InnerText = _docXML.Curcy;
                node53.AppendChild(node63);
                XmlNode node64 = doc.CreateElement("ЦенаВВалюте");
                node64.InnerText = _docXML.priceNoVat.GetItem(k);
                node53.AppendChild(node64);
                XmlNode node65 = doc.CreateElement("ПредДопРознЦена");
                node53.AppendChild(node65);
                XmlNode node66 = doc.CreateElement("Код1");
                node53.AppendChild(node66);
                XmlNode node67 = doc.CreateElement("Код2");
                node53.AppendChild(node67);
                XmlNode node68 = doc.CreateElement("Код3");
                node53.AppendChild(node68);
            }
            doc.Save(_newFilePath);

            Logger.FileProcessed(_fileName, _newFilePath);
            MoveFile(Settings.folderXML);
        }
    }
}
