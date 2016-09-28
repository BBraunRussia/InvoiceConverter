using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;
using InvoiceConverter.Domain.Mails;
using InvoiceConverter.Domain.Common;

namespace InvoiceConverter.Domain.Logger
{
    public static class LoggerManager
    {
        private static ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.ColoredConsole()
            .WriteTo.RollingFile(@"Log\Log-{Date}.txt")
            .CreateLogger();

        public static ILogger Logger { get { return logger; } }

        public static void FileProcessed(string file, string fileNew)
        {
            Write(string.Concat("Файл ", file, " был конвертирован в файл ", fileNew));
        }

        public static void ErrorCreated(string fileName, string error)
        {
            Write(string.Concat("Ошибка при обработке файла ", fileName, " - ", error));
            Sender.SendMail(Settings.adminEmail, "Ошибка при обработке файла", "Ошибка при обработке файла, описание ошибки в лог файле");
        }

        public static void FolderCreated(string folder)
        {
            Write("Создание папки " + folder);
        }
        
        private static void Write(string Message)
        {
            using (StreamWriter swLog = new StreamWriter("log.txt", true, Encoding.Unicode))
            {
                swLog.WriteLine(string.Concat(DateTime.Now.ToString(), ": ", Message));
            }
        }
    }
}
