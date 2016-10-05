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
    }
}
