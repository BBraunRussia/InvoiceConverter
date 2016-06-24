using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceConverter
{
    public class MyFile
    {
        string _custNumberSAP;

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
                Logger.ErrorCreated(filePath, err.Message);
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
                Logger.FileProcessed(fileName, newFilePath);
            }
            catch(Exception err)
            {
                Logger.ErrorCreated(newFilePath, err.Message);
            }
        }

        private string CreateFilePathWithCustNumber(string folder, string file)
        {
            return CreateFilePath(WorkWithString.CreateString(folder, @"\", _custNumberSAP), file);
        }

        private static string CreateFilePath(string folder, string file)
        {
            return WorkWithString.CreateString(folder, @"\", file);
        }

        private static void DeleteIfExistFile(string newFilePath)
        {
            if (File.Exists(newFilePath))
                File.Delete(newFilePath);
        }

        public static string[] GetFiles()
        {
            try
            {
                string[] files = Directory.GetFiles(Settings.folderNew, "*.xml", SearchOption.TopDirectoryOnly);
                if (files.Count() == 0)
                    return null;
                return files;
            }
            catch (Exception err)
            {
                Logger.ErrorCreated(Settings.folderNew, err.Message);
                return null;
            }
        }

        public static void CreateFolder(string fileName)
        {
            string pathOnly = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(pathOnly))
            {
                Directory.CreateDirectory(pathOnly);
                Logger.FolderCreated(pathOnly);
            }
        }
    }
}
