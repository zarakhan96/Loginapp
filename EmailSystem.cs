namespace Loginapp.Services
{
    public class EmailSystem
    {
        private readonly string smtpServer;
        private readonly int port;
        private readonly string senderEmail;
        private readonly string senderPassword;

        public EmailSystem(string smtpServer, int port, string senderEmail, string senderPassword)
        {
            this.smtpServer = smtpServer;
            this.port = port;
            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using var client = new System.Net.Mail.SmtpClient(smtpServer, port)
                {
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword)
                };

                var message = new System.Net.Mail.MailMessage(senderEmail, to, subject, body)
                {
                    IsBodyHtml = true
                };

                await client.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
