using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Converter
{
    public static class Logger
    {
        public static void FileProcessed(string file, string fileNew)
        {
            Write(WorkWithString.CreateString("Файл ", file, " был конвертирован в файл ", fileNew));
        }

        public static void ErrorCreated(string fileName, string error)
        {
            Write(WorkWithString.CreateString("Ошибка при обработке файла ", fileName, " - ", error));
            Sender.SendMail(error);
        }

        public static void FolderCreated(string folder)
        {
            Write("Создание папки " + folder);
        }

        public static void BeginWork()
        {
            Write("Обработка началась");
        }

        public static void EndWork()
        {
            Write("Обработка завершена");
        }

        private static void Write(string Message)
        {
            using (StreamWriter swLog = new StreamWriter("log.txt", true, Encoding.Unicode))
            {
                swLog.WriteLine(WorkWithString.CreateString(DateTime.Now.ToString(), ": ", Message));
            }
        }
    }
}
