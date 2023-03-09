using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Common
{
    public class EmailSender : IEmailSender
    {
        private Email _Email;
        public EmailSender(IOptions<Email>email)
        {
            _Email = email.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Execute(email, subject, htmlMessage).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string email, string subject, string messege)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _Email.ToEmail : email;
                MailMessage Mail = new MailMessage()
                {
                    From = new MailAddress(_Email.UsernameEmail, "My Email Name")
                };
                Mail.To.Add(new MailAddress(toEmail));
                Mail.CC.Add(new MailAddress(_Email.CcEmail));
                Mail.Subject = "Task Management System:" + subject;
                Mail.Body = messege;
                Mail.IsBodyHtml = true;
                Mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_Email.PrimaryDomain, _Email.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_Email.UsernameEmail, _Email.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(Mail);
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }
    }
}
