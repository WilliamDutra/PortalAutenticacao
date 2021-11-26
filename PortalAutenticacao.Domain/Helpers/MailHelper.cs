using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PortalAutenticacao.Domain.Helpers
{
    public class MailHelper
    {
        private SmtpClient _SmtpClient;

        public MailHelper()
        {
            _SmtpClient = new SmtpClient("smtp.sendgrid.net");
        }

        public void EnviarEmail(string Destinatario, string Assunto, string Mensagem)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = Assunto;
                message.To.Add(Destinatario);
                message.Body = Mensagem;
                message.From = new MailAddress("", "Portal de Autenticação");


                _SmtpClient.UseDefaultCredentials = false;
                _SmtpClient.Credentials = new NetworkCredential("apikey", "");
                _SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                _SmtpClient.Port = 587;
                _SmtpClient.Timeout = 99999;
                _SmtpClient.EnableSsl = false;
                _SmtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
