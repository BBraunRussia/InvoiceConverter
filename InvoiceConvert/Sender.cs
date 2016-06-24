using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Converter
{
    public static class Sender
    {
        private static List<string> emails;
        private static string password = ""; //пароль на почтовом сервере
        private static string userName = ""; //логин на почтовом сервере
        private static string host = "212.0.16.135";
        private static int port = 25;
        private static string subject = "Ошибка при конвертации файла"; //заголовок
        private static string fromAddress = "robot.ru@bbraun.com"; //адрес отправителя

        static Sender()
        {
            emails = new List<string>();
        }

        public static void AddEmails(string stringEmails)
        {
            string[] splitEmails = stringEmails.Split(',');
            foreach (string email in splitEmails)
                emails.Add(email.Trim());
        }

        public static void SendMail(string error)
        {
            try
            {
                string body = error + "\nПолная информация в лог-файле."; //письмо                
                
                MailMessage msg = new MailMessage(new MailAddress(fromAddress), new MailAddress(emails[0]));

                foreach (string item in emails)
                {
                    if (item != emails[0])
                        msg.To.Add(new MailAddress(item));
                }

                msg.Subject = subject;

                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;

                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = false;
                SmtpClient client = new SmtpClient(host, port);

                client.Credentials = new System.Net.NetworkCredential(userName, password);

                //client.EnableSsl = true;

                client.Send(msg);
                msg.Dispose();
            }
            catch
            {
            }
        }
    }
}
