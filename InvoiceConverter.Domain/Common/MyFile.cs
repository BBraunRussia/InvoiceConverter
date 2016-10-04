using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InvoiceConverter.Domain.Logger;

namespace InvoiceConverter.Domain.Common
{
    public class MyFile
    {
        private string _custNumberSAP;

        public MyFile(string custNumberSAP)
        {
            _custNumberSAP = custNumberSAP;
        }

        public string MoveFile(string filePath, string xmlFolderTo)
        {
            string fileName = Path.GetFileName(filePath);
            string newFilePath = GetFilePathWithCustNumber(xmlFolderTo, fileName);
            Move(filePath, newFilePath);
            return newFilePath;
        }
        
        private static void Move(string filePath, string newFilePath)
        {
            try
            {
                CreateFolder(newFilePath);
                DeleteIfExistFile(newFilePath);
                File.Move(filePath, newFilePath);
            }
            catch (Exception err)
            {
                LoggerManager.Logger.Error(err, "Ошибка при обработке файла {filename}", filePath);
            }
        }

        public void Copy(string filePath, string xmlFolderTo, string newFileName)
        {
            string newFilePath = GetFilePathWithCustNumber(xmlFolderTo, newFileName + ".xml");

            try
            {
                DeleteIfExistFile(newFilePath);

                File.Copy(filePath, newFilePath, true);
                LoggerManager.Logger.Information("Файл {filePath} скопирован {newfilename}", filePath, newFilePath);
            }
            catch(Exception err)
            {
                LoggerManager.Logger.Error(err, "Ошибка при обработке файла {filename}", newFilePath);
            }
        }

        public string GetFilePathWithCustNumber(string folder, string file)
        {
            return CreateFilePath(string.Concat(folder, @"\", _custNumberSAP), file);
        }

        private static string CreateFilePath(string folder, string file)
        {
            return string.Concat(folder, @"\", file);
        }

        private static void DeleteIfExistFile(string newFilePath)
        {
            if (File.Exists(newFilePath))
                File.Delete(newFilePath);
        }

        public static string[] GetFiles(string folder, string filter = "*.*")
        {
            try
            {
                return Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException err)
            {
                LoggerManager.Logger.Error(err, "Ошибка {folder}", Settings.folderNew);
                return null;
            }
        }

        public static string[] GetDirectories(string folder)
        {
            try
            {
                return Directory.GetDirectories(folder);
            }
            catch (UnauthorizedAccessException err)
            {
                LoggerManager.Logger.Error(err, "Ошибка {folder}", Settings.folderNew);
                return null;
            }
        }

        public static void CreateFolder(string fileName)
        {
            string pathOnly = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(pathOnly))
            {
                Directory.CreateDirectory(pathOnly);
                LoggerManager.Logger.Information("Папка создана {folder}", pathOnly);
            }
        }
    }
}
