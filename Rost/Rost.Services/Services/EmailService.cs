using MimeKit;
using System.Threading.Tasks;
using Rost.Services.Infrastructure;
using MailKit.Net.Smtp;

namespace Rost.Services.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "noreply@xn--n1adee.xn--d1acj3b"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = $"<p>{message}</p>" };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("mail.nic.ru", 465, true);
            await client.AuthenticateAsync("noreply@xn--n1adee.xn--d1acj3b", "NoreplyRost1");
            await client.SendAsync(emailMessage);
 
            await client.DisconnectAsync(true);

            /*
            MailAddress from = new MailAddress("noreply@рост.дети", "noreply@рост.дети");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("vladimir.rakhimov@gmail.com", "SummerTime2020!"),

            };
            using (var mail = new MailMessage("vladimir.rakhimov@gmail.com", email)
            {
                Subject = subject,
                Body = message
            })
            {
                smtp.Send(mail);
            }
            
            
            m.Subject = subject;
            m.Body = message;
            SmtpClient smtp = new SmtpClient("mail.nic.ru", 465);
            smtp.Credentials = new NetworkCredential("noreply@рост.дети", "NoreplyRost1");
            smtp.EnableSsl = true;
            
            try
            {
                await smtp.SendMailAsync(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
        }
    }
}