using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class EmailService
    {

        public bool EnviarEmail(string Assunto, string Nome, string Menssagem, string Email, string Telefone)
        {
            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress("looksalice.comunicacao@gmail.com");
                mail.To.Add("looksalice.adm@gmail.com");
                mail.Subject = Assunto;
                mail.Body = "<h1>" + Nome + " diz:</h1><br><p>" + Menssagem + "</p><br/>" +
                    "<h3>Infos</h3>" +
                    "<p>Nome:" + Nome + "</p>" +
                    "<p>Email:" + Email + "</p>" +
                    "<p>Telefone:" + Telefone + "</p>";
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("looksalice.comunicacao@gmail.com", "Looksalice2020*");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                return true;

            }
        }


        public bool Verificacao(string Assunto, string Nome, string Menssagem, string Email, string Telefone)
        {
            if (string.IsNullOrEmpty(Assunto))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(Nome))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(Menssagem))
            {
                return false;

            }
            else if (string.IsNullOrEmpty(Email))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(Telefone))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}