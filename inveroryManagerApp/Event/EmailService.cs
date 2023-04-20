using System.Net.Mail;
using System.Net;

namespace inveroryManagerApp.Event
{
  public class EmailService
  {
        public void OnOrderProcessed(object sender, OrderProcessedEventArgs e)
        {
            // Send an email notification
            string customerEmail = "kayden@bbd.co.za";
            string subject = "Your order has been processed";
            string body = $"Thank you for your order Your order has been processed " +
                            $"and your total amount is {e.ProcessedOrder.Quantity:C}.";

            // Create a new SmtpClient instance and configure it with the mail server details
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("kaydenkara@gmail.com", "pstijvbrwrgddofm");
            client.EnableSsl = true;

            // Create a new MailMessage instance and configure it with the email details
            MailMessage message = new MailMessage();
            message.From = new MailAddress("kaydenkara@gmail.com");
            message.To.Add(new MailAddress(customerEmail));
            message.Subject = subject;
            message.Body = body;

            // Send the email using the SmtpClient.Send method
            client.Send(message);

            Console.WriteLine($"Sent email to {customerEmail}: {subject}");
        }
    }
}
