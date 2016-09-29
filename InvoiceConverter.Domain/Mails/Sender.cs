using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace InvoiceConverter.Domain.Mails
{
    public static class Sender
    {
        private static string password = ""; //пароль на почтовом сервере
        private static string userName = ""; //логин на почтовом сервере
        private static string host = "212.0.16.135";
        private static int port = 25;
        //private static string subject = "Ошибка при конвертации файла"; //заголовок
        private static string fromAddress = "robot.ru@bbraun.com"; //адрес отправителя
        
        private static string[] GetEmails(string stringEmails)
        {
            return stringEmails.Split('\n');
        }

        public static void SendMail(string stringEmails, string subject, string body, string filePath = null)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromAddress);

            //string[] emails = GetEmails(stringEmails);
            string[] emails = new string[] { "pavel.maslyaev@bbraun.com" };
            foreach (string email in emails)
            {
                msg.To.Add(new MailAddress(email));
            }

            msg.Subject = subject;

            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;

            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            //прикрепляем файл если задан путь
            if (filePath != null)
                msg.Attachments.Add(new Attachment(filePath));

            SmtpClient client = new SmtpClient(host, port);

            client.Credentials = new System.Net.NetworkCredential(userName, password);

            client.Send(msg);
            msg.Dispose();
        }
    }
}
