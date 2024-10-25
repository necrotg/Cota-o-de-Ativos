using Cotação_de_Ativos.Utils;
using NLog;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Mail;
using System.Text;
public class EmailService
{
    static ILogger logger = LogManager.GetCurrentClassLogger();
    public void sendEmail(String bodyDoEmail)
        {

            try
            {
                string host = ConfigurationManager.AppSettings["SmtpHost"];
                int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string username = ConfigurationManager.AppSettings["SmtpUsername"];
                string password = ConfigurationManager.AppSettings["SmtpPassword"];
                string to = ConfigurationManager.AppSettings["To"];
                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
                ValidateConfigParameters(host, port, username, password, to);
                SmtpClient smtpClient = new SmtpClient(host, port);

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailAddress mailAddressFrom = new MailAddress(username,Constants.EMAIL_DISPLT_NAME);
                MailAddress mailAddressTo = new MailAddress(to);
                MailMessage mailMessage = new MailMessage(mailAddressFrom, mailAddressTo);
            
                mailMessage.Subject = Constants.EMAIL_SUBJECT;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = bodyDoEmail;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                smtpClient.Send(mailMessage);
                logger.Info(Constants.SUCCESS_EMAIL_SENT);
            }

            catch (Exception e)
            {
                logger.Error(e.ToString());
                throw new Exception("Ocorreu um problema ao enviar email: " + e.Message);
            }
        }

    private void ValidateConfigParameters(string? host, int port, string? username, string? password, string? to)
    {
        if (host == null) throw new ArgumentNullException("host"); 
        if (port == 0) throw new ArgumentNullException("password");  
        if (username == null) throw new ArgumentNullException("to");  
        if (password == null) throw new ArgumentNullException("host");
        if (to == null) throw new ArgumentNullException("host");
    }
}
        

