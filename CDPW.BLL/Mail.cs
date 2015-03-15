using System;
//using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;

namespace CDPW.BLL
{
    public class Mail
    {
        /// <summary>
        /// Send e-mail messages.
        /// </summary>
        /// <param name="To"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="Priority"></param>
        /// 
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Send(string To, string cc, string bcc, string subject, string body, MailPriority Priority)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            var conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            var appsettings = (AppSettingsSection)conf.GetSection("appSettings");
            var mailFromTitleString = appsettings.Settings["MailFromTitle"].ToString() != null ? appsettings.Settings["MailFromTitle"].Value.ToString() : string.Empty;

            SmtpSection smtps = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage())
            {
                mm.To.Add(new MailAddress(To));
                if (!string.IsNullOrEmpty(cc)) mm.CC.Add(new MailAddress(cc));
                if (!string.IsNullOrEmpty(bcc)) mm.Bcc.Add(new MailAddress(bcc));
                mm.IsBodyHtml = true;
                mm.Priority = Priority;
                mm.BodyEncoding = System.Text.Encoding.UTF8;
                mm.Subject = subject;
                mm.Body = body;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Send(mm);

                SmtpClient sc = new SmtpClient(smtps.Network.Host, smtps.Network.Port);
                sc.UseDefaultCredentials = smtps.Network.DefaultCredentials;
                sc.Credentials = new System.Net.NetworkCredential(smtps.Network.UserName, smtps.Network.Password);
                sc.Send(mm);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }
    }
}
