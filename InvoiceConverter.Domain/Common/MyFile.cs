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

        public void MoveFile(string xmlFolder, string fileName)
        {
            string newFilePath = CreateFilePathWithCustNumber(xmlFolder, fileName);
            Move(fileName, newFilePath);
        }

        public static void MoveFileError(string fileName)
        {
            string newFilePath = CreateFilePath(Settings.folderXMLError, fileName);
            Move(fileName, newFilePath);
        }

        private static void Move(string fileName, string newFilePath)
        {
            string filePath = CreateFilePath(Settings.folderNew, fileName);

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

        public void Copy(string fileName, string newFileName)
        {
            newFileName = newFileName + ".xml";
            string filePath = CreateFilePath(Settings.folderNew, fileName);
            string newFilePath = CreateFilePathWithCustNumber(Settings.folderConv, newFileName);

            try
            {
                DeleteIfExistFile(newFilePath);

                File.Copy(filePath, newFilePath, true);
                LoggerManager.Logger.Information("Файл {filename} скопирован {newfilename}", fileName, newFilePath);
            }
            catch(Exception err)
            {
                LoggerManager.Logger.Error(err, "Ошибка при обработке файла {filename}", newFilePath);
            }
        }

        private string CreateFilePathWithCustNumber(string folder, string file)
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
