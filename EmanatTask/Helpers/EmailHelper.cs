using EmanatTask.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace EmanatTask.Helpers
{
    public class EmailHelper
    {
        private readonly EmailConfiguration mailSettings;
        public EmailHelper(EmailConfiguration mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        public async Task Send(string emailAdress, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailSettings.From),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(emailAdress));

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtp.ConnectAsync(mailSettings.SmtpServer, 587, false).ConfigureAwait(false);
            smtp.Authenticate(mailSettings.From, mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
