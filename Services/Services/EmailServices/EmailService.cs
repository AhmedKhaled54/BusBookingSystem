using Data.Helper;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting _mail;
        public EmailService(MailSetting mail)
        {
            _mail = mail;
        }
        public async Task SendEmail(string Subject, string Body, string ToEmail)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mail.Email);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = Subject;

            var builder=new BodyBuilder();
            builder.HtmlBody = Body;
            email.Body=builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mail.Host, _mail.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mail.Email, _mail.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);


        }
    }
}
