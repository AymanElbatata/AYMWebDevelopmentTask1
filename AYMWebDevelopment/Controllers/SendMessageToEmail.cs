using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net.Mail;
using System.IO;

namespace AYMWebDevelopment.Controllers
{
    public class SendMessageToEmail
    {
        //mysalesDataContext db = new mysalesDataContext();

        public static void sendemailtosomeone(string emailto, string subject, string bodymsg)
        {
            MailMessage mail = new MailMessage("ayman.fathy.elbatata@gmail.com", emailto);
            mail.Subject = subject;
            mail.Body = bodymsg;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "",
                Password = ""
            };

            smtpClient.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                var bb = e;
                throw;
            }
            
        }


        public static void sendemailtosomeone(string emailto, string subject, string bodymsg, HttpPostedFileBase Image1 = null)
        {
            MailMessage mail = new MailMessage("ayman.fathy.elbatata@gmail.com", emailto);
            mail.Subject = subject;
            mail.Body = bodymsg;

            if (Image1 != null)
            {
                string fileName = Path.GetFileName(Image1.FileName);
                mail.Attachments.Add(new Attachment(Image1.InputStream, fileName));

            }

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "",
                Password = ""
            };

            smtpClient.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                var bb = e;
                throw;
            }

        }

    }
}
