﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Net.Mail;
using System.Data.OleDb;
using InvoiceConverter.Domain.Logger;
using InvoiceConverter.Domain.Common;
using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Companies;

namespace InvoiceConverter
{
    class Program
    {
        static int Main(string[] args)
        {
            LoggerManager.Logger.Debug("Начинаю поиск файлов");
            string[] filePaths = MyFile.GetFiles();
            LoggerManager.Logger.Debug("Поиск файлов завершён");
            if (filePaths == null)
            {
                LoggerManager.Logger.Debug("Файлы не найдены");
                return 0;
            }
            
            foreach (string filePath in filePaths)
            {
                LoggerManager.Logger.Information("Обработка началась");

                var docXML = new DocXML(filePath);

                string fileName = Path.GetFileName(filePath); // имя файла

                try
                {
                    docXML.fillFields();
                }
                catch (Exception err)
                {
                    LoggerManager.Logger.Error(err, "Error in {filename}", fileName);
                    
                    MyFile.MoveFileError(fileName);

                    continue;
                }

                MyFile myFile = new MyFile(docXML.CustNumberSAP);
                
                bool move = true;

                string newFileName = string.Concat(docXML.Invoice, "_", docXML.InvoiceDate);

                ConvFile file = null;

                LoggerManager.Logger.Debug("Покупатель " + docXML.Customer);

                switch (docXML.Customer)
                {
                    case Cust.AnteyFarma:
                        file = new AnteyFarma(fileName, docXML);
                        move = false;
                        break;
                    case Cust.AptekaSkladChel:
                        file = new AptekaSkladChel(fileName, docXML);
                        break;
                    case Cust.GrandCapital:
                        file = new GrandCapital(fileName, docXML);
                        break;
                    case Cust.FarmTreid:
                        file = new FarmTreyd(fileName, docXML);
                        break;
                    case Cust.Protek:
                        newFileName = string.Concat(docXML.Invoice);
                        myFile.Copy(fileName, newFileName);
                        break;
                    case Cust.SeveroZapad:
                        file = new SeveroZapad(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Shaklin:
                        file = new Shaklin(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Voltars:
                        file = new Voltars(fileName, docXML);
                        move = false;
                        break;
                    case Cust.UralApteka:
                        file = new UralApteka(fileName, docXML);
                        move = false;
                        break;
                    case Cust.Katren:
                        file = new Katren(filePath, docXML);
                        break;
                    case Cust.GBUZ:
                    case Cust.Optimed:
                    case Cust.Optimed2:
                    case Cust.EuropeanMedicalCenter:
                    default:
                        myFile.Copy(fileName, newFileName);
                        break;
                }

                if (file != null)
                {
                    file.CreateAndSaveFile();
                }

                if (move)
                {
                    LoggerManager.Logger.Debug("Готов к перемещению файла {filename}", fileName);
                    myFile.MoveFile(Settings.folderXML, fileName);
                    LoggerManager.Logger.Debug("Файл перемещён");
                }
            }

            if (filePaths.Count() > 0)
            {
                LoggerManager.Logger.Information("Обработка завершилась");
            }

            LoggerManager.Logger.Debug("Программа корректно завершила работу");
            return 1;
        }
    }
}
