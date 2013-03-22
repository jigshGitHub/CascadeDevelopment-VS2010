using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
namespace Cascade.Helpers
{
    public class Emailer
    {
        private static readonly string thisClass = "Cascade.Helpers.Emailer";
        public static SmtpClient SMTPClient;
        static Emailer()
        {
            SMTPClient = new SmtpClient();
            IntilializeSMTPClient(SMTPClient);
        }

        public static void IntilializeSMTPClient(SmtpClient smtpClient)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = "Configuring SmtpClient";
            LogHelper.Info(logMessage);

            try
            {
                if (smtpClient == null)
                    smtpClient = new SmtpClient();
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["Server.UseDefaultCredentials"].ToString()))
                    smtpClient.UseDefaultCredentials = true;
                smtpClient.Host = ConfigurationManager.AppSettings["Server.Hostname"].ToString();// "smtp.cso.local";
                smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["Server.Port"].ToString());//25;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["Server.CredentialsRequired"].ToString()))
                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Server.Username"].ToString(), ConfigurationManager.AppSettings["Server.Password"].ToString());
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while configuring SmtpClient", ex);
            }
        }

        public static MailMessage CreateMessage( string subject, List<string> toEmailIds, string bodyText,string fromEmailId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = "Creating MailMessage";
            LogHelper.Info(logMessage);

            MailMessage message = null;
            if (fromEmailId == "")
                fromEmailId = ConfigurationManager.AppSettings["emailSenderAccount"];

            try
            {
                message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(fromEmailId);
                message.Subject = subject;
                message.Body = bodyText;
                foreach(string toEmailId in toEmailIds)
                    message.To.Add(toEmailId);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while creating MailMessage", ex);
            }


            return message;
        }

        public static bool SendMessage(MailMessage message)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = "Sending MailMessage";
            LogHelper.Info(logMessage);

            try
            {
                if (message == null)
                    throw new Exception("Required properties for MailMessage not set.");
                SMTPClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while sending MailMessage", ex);
            }
            return false;
        }
    }
}
