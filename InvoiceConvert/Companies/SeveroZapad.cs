using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/* XLS */

namespace InvoiceConverter.Companies
{
    public class SeveroZapad : ConvFile
    {
        private const int INDEX_ROW_BEGIN_HEADER = 0;
        private const int INDEX_COLUMN_BEGIN_HEADER = 0;
        private const int INDEX_BEGIN = 2;

        public SeveroZapad(string sourceFile, DocXML docXML) :
            base(sourceFile, docXML) { }

        protected override void CreateFileName()
        {
            _newFileName = string.Concat(_docXML.Torg12, ".xls");
            CreateNewPath();
        }

        public override void CreateAndSaveFile()
        {
            try
            {
                using (FileStream stream = new FileStream(_newFilePath, FileMode.OpenOrCreate))
                {
                    int rowIndex = INDEX_BEGIN;

                    ExcelWriter writer = new ExcelWriter(stream);
                    writer.BeginWrite();

                    FillHeader(writer);

                    for (int i = 0; i < _docXML.idTnrProductCode.Count; i++)
                    {
                        int columnIndex = INDEX_COLUMN_BEGIN_HEADER;

                        writer.WriteCell(rowIndex, columnIndex, _docXML.idTnrProductCode.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.kText.GetItem(i));
                        columnIndex += 2;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.kText.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.menee.GetItem(i));
                        columnIndex += 2;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.idTnrBarCode.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.idTnrSerialNumber.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.gtdNo.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.vfDat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.hsDat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.mfName1.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.menge.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.priceNoVat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.actManPrRub.GetItem(i));
                        columnIndex += 5;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.gtdHerkl.GetItem(i));
                        columnIndex += 3;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.mengNoVat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.vatrate.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.totalVat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.mengVat.GetItem(i));
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, "RUS");
                        columnIndex += 2;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.Torg12);
                        columnIndex++;
                        writer.WriteCell(rowIndex, columnIndex, _docXML.Invoice);
                    }

                    writer.EndWrite();
                }

                Logger.FileProcessed(_fileName, _newFilePath);
                MoveFile(Settings.folderXML);
            }
            catch (Exception err)
            {
                MyFile.MoveFileError(_fileName);
                Logger.ErrorCreated(_fileName, err.Message);
            }
        }

        private void FillHeader(ExcelWriter writer)
        {
            int rowIndex = INDEX_ROW_BEGIN_HEADER;
            int columnIndex = INDEX_COLUMN_BEGIN_HEADER;

            writer.WriteCell(rowIndex, columnIndex, "Код номенклатуры");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Наименование номенклатуры");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "МНН");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Торговое наименование");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Ед.измерения (наименование)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Код по ОКЕИ");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Штрих-код");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Серийный номер");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "ГТД");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Срок годности");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Дата производства");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Производитель");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Кол-во");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Цена без НДС");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Цена производителя");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Зарегестрированная цена (для ЖВ)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Скидка, %");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Скидка, руб");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Цена со скидкой");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Страна (страна производства)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Страна (по ISO)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Страна (код по ОКСМ)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Сумма без учета НДС");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Ставка НДС, %");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Сумма НДС");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Сумма с учетом НДС");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Код валюты (ISO)");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Номер заказа");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Номер Торг-12");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Номер счета-фактуры");

            rowIndex++;
            columnIndex = INDEX_COLUMN_BEGIN_HEADER;

            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Дата");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Дата");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Число");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
            columnIndex++;
            writer.WriteCell(rowIndex, columnIndex, "Строка");
        }
    }
}
