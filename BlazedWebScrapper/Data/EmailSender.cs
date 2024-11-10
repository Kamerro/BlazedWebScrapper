using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebScrapper;

namespace BlazedWebScrapper.Data
{
    public class EmailSender
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private EmailParams _emailParams { get; set; }

        // podstawowy konstruktor
        public EmailSender()
        {
            EmailParams emailParams = new EmailParams()
            {
                HostSmtp = "poczta.interia.pl",
                Port = 587,
                EnableSsl = true,
                SenderName = "Webscrapper Info",
                SenderEmail = "webscrapper.mail@interia.pl",
                SenderEmailPassword = "WEBscrapper123!"
            };

            _emailParams = emailParams;
        }

        // nadpisany konstruktor
        public EmailSender(EmailParams emailParams)
        {
            _emailParams = emailParams;
        }

        public async Task Send(string subject, string body, string to)
        {
            _mail = new MailMessage();
            _mail.From = new MailAddress(_emailParams.SenderEmail, _emailParams.SenderName);
            _mail.To.Add(new MailAddress(to));
            _mail.IsBodyHtml = true;
            _mail.Subject = subject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;
            _mail.Body = body;

            _smtp = new SmtpClient
            {
                Host = _emailParams.HostSmtp,
                EnableSsl = _emailParams.EnableSsl,
                Port = _emailParams.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailParams.SenderEmail, _emailParams.SenderEmailPassword)
            };

            _smtp.SendCompleted += OnSendCompleted;

            await _smtp.SendMailAsync(_mail);
        }

        private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }
    }
}