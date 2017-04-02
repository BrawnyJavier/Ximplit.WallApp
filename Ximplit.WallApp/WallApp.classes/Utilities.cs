using System;
using System.Net.Mail;
namespace Miscellaneous
{
    public class EmailSender
    {
        public void SendEmail(string EmailBody, string Subject, string Mail)
        {
            MailMessage MailMessage = new MailMessage("Ximplit.WallApp@gmail.com", Mail);
            MailMessage.Subject = Subject;
            MailMessage.Body = EmailBody;
            SmtpClient SmtpClient = new SmtpClient("smtp.gmail.com", 587);
            SmtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "Ximplit.WallApp@gmail.com",
                Password = "insertpasswordhere"
            };
            SmtpClient.EnableSsl = true;
            SmtpClient.Send(MailMessage);
        }
       
    }
    public class Formaters
    {
        public string TimelineDateFormat(DateTime dateToFormat)
        {
         
            TimeSpan difference = DateTime.Now - dateToFormat;
            var segundos = difference.TotalSeconds;
            var interval = Math.Floor(segundos / 31536000);

            if (interval > 1)
            {
                return interval + " años";
            }
            interval = Math.Floor(segundos / 2592000);
            if (interval > 1)
            {
                return interval + " meses";
            }
            interval = Math.Floor(segundos / 86400);
            if (interval > 1)
            {
                return interval + " días";
            }
            interval = Math.Floor(segundos / 3600);
            if (interval > 1)
            {
                return interval + " horas";
            }
            interval = Math.Floor(segundos / 60);
            if (interval > 1)
            {
                return interval + " minutos";
            }
            return Math.Floor(segundos) + " segundos";
            
        }
    }
}
