using System;
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
using InvoiceConverter.Domain.Entities;
using InvoiceConverter.Domain.Mails;
using InvoiceConverter.Domain.Infractructure;

namespace InvoiceConverter
{
    class Program
    {
        static int Main(string[] args)
        {
            string[] filePaths = GetFiles();
            
            foreach (string filePath in filePaths)
            {
                LoggerManager.Logger.Information("Обработка началась");

                DocXML docXML = null;
                try
                {
                    docXML = new DocXML(filePath);
                }
                catch (NullReferenceException ex)
                {
                    LoggerManager.Logger.Error(ex, "Проверьте присутствие покупателя в БД");
                    continue;
                }
                
                MyFile myFile = new MyFile(docXML.Customer.Number);
                bool move = true;

                string newFileName = string.Concat(docXML.Invoice, "_", docXML.InvoiceDate);

                ConvFile file = null;

                LoggerManager.Logger.Debug("Покупатель " + docXML.Customer.Name);

                switch (docXML.Customer.Number)
                {
                    case "0020440063": //Антей
                        file = new AnteyFarma(filePath, docXML);
                        move = false;
                        break;
                    case "0020564803": //Аптечный Склад Челябинск
                        file = new AptekaSkladChel(filePath, docXML);
                        break;
                    case "0020439554": //ФК Гранд Капитал ООО
                        file = new GrandCapital(filePath, docXML);
                        break;
                    case "0020439922": //Фарм-Трейд ООО
                        file = new FarmTreyd(filePath, docXML);
                        break;
                    case "0020438427": //Протек
                        newFileName = string.Concat(docXML.Invoice);
                        myFile.Copy(filePath, Settings.folderConv, newFileName);
                        break;
                    case "0020439679": //ЗАО Северо-Запад
                        file = new SeveroZapad(filePath, docXML);
                        move = false;
                        break;
                    case "0020439066": //Шаклин
                        file = new Shaklin(filePath, docXML);
                        move = false;
                        break;
                    case "0020438640": //Волтарс
                        file = new Voltars(filePath, docXML);
                        move = false;
                        break;
                    case "0020441355": //АптекаУрал
                        file = new UralApteka(filePath, docXML);
                        move = false;
                        break;
                    case "0020439804": //Катрен
                        file = new Katren(filePath, docXML);
                        break;
                    default:
                        myFile.Copy(filePath, Settings.folderConv, newFileName);
                        break;
                }

                if (file != null)
                {
                    file.CreateAndSaveFile();
                }

                if (move)
                {
                    myFile.MoveFile(filePath, Settings.folderXML);
                    LoggerManager.Logger.Debug("Файл {filePath} перемещён в папку {filePath2}", filePath, Settings.folderXML);
                }
            }

            if (filePaths.Any())
            {
                LoggerManager.Logger.Information("Обработка завершилась");
            }

            SendMails();
            
            LoggerManager.Logger.Debug("Программа корректно завершила работу");
            return 1;
        }

        private static string[] GetFiles()
        {
            string[] filePaths = null;
            try
            {
                filePaths = MyFile.GetFiles(Settings.folderNew, "*.xml");
            }
            catch (NullReferenceException err)
            {
                LoggerManager.Logger.Error(err, "Ошибка при установке настроек");
            }

            if (!filePaths.Any())
            {
                LoggerManager.Logger.Debug("Файлы не найдены");
            }

            return filePaths;
        }

        private static void SendMails()
        {
            string[] dirPaths = MyFile.GetDirectories(Settings.folderConv);

            ICustomerRepository customerRepository = CompositionRoot.Resolve<ICustomerRepository>();
            
            LoggerManager.Logger.Debug("Начинаю поиск файлов для отправки");
            foreach (string dirPath in dirPaths)
            {
                string dirName = Path.GetFileName(dirPath);
                Customer customer = customerRepository.Customers.FirstOrDefault(cust => ((cust.Number == dirName) && (cust.Enable)) );
                if (customer == null)
                {
                    continue;
                }

                string[] filePaths = MyFile.GetFiles(dirPath);

                MailToCustomer mailToCustomer = new MailToCustomer(customer);

                foreach (string filePath in filePaths)
                {
                    mailToCustomer.SendMail(filePath);
                    LoggerManager.Logger.Information("Письмо с файлом {filePath} отправлено", filePath);
                                        
                    MyFile myFile = new MyFile(customer.Number);
                    myFile.MoveFile(filePath, Settings.folderSent);

                    LoggerManager.Logger.Debug("Файл {filePath} перемещён в папку {folder}", filePath, Settings.folderSent);
                }
            }

            LoggerManager.Logger.Debug("Рассылка завершилась");
        }
    }
}
