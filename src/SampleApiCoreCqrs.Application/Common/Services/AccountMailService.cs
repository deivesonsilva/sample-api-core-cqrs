using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Model;
using SampleApiCoreCqrs.Infrastructure.Entities;

namespace SampleApiCoreCqrs.Application.Common.Services
{
    public class AccountMailService : IAccountMailService
    {
        private readonly IOptions<EmailConfiguration> _config;

        public AccountMailService(IOptions<EmailConfiguration> config)
        {
            _config = config;
        }

        public async void SendRegisterAsync(Account entity, string verifyCode)
        {
            if (!_config.Value.IsActive)
                return;

            try
            {
                StringBuilder body = new StringBuilder();
                body.AppendLine(string.Format("{0},", entity.FirstName));
                body.AppendLine(string.Format("<p>Utilize o código <b>{0}</b> para confirmar seu cadastro.</p>", verifyCode));
                body.AppendLine("-&nbsp; SampleApi &nbsp;-");

                MailMessage message = new MailMessage();
                message.Sender = new MailAddress(_config.Value.Email);
                message.From = new MailAddress(_config.Value.Email);
                message.To.Add(new MailAddress(entity.Email));

                message.Subject = string.Format("Confirmação de cadastro");
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = body.ToString();

                SmtpClient smtpClient = GetClient();
                await smtpClient.SendMailAsync(message);
                smtpClient.Dispose();
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void SendResetPasswordAsync(Account entity, string resetPassword)
        {
            if (!_config.Value.IsActive)
                return;

            try
            {
                StringBuilder body = new StringBuilder();
                body.AppendLine(string.Format("{0},", entity.FirstName));
                body.AppendLine(string.Format("<p>Utilize o código <b>{0}</b> como senha temporaria, em seguida será solicitado uma nova senha.</p>", resetPassword));
                body.AppendLine("-&nbsp; SampleApi &nbsp;-");

                MailMessage message = new MailMessage();
                message.Sender = new MailAddress(_config.Value.Email);
                message.From = new MailAddress(_config.Value.Email);
                message.To.Add(new MailAddress(entity.Email));

                message.Subject = string.Format("Recuperar Senha");
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = body.ToString();

                SmtpClient smtpClient = GetClient();
                await smtpClient.SendMailAsync(message);
                smtpClient.Dispose();
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SmtpClient GetClient()
        {
            return new SmtpClient
            {
                Host = _config.Value.Host,
                Port = _config.Value.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.Value.Email, _config.Value.Password)
            };
        }
    }
}
