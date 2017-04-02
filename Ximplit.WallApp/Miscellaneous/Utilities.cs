using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
            SmtpClient.Send(MailMessage);
        }
    }
}
