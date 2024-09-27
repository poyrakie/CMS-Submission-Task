using Azure.Communication.Email;
using Azure;

namespace SubmissionTask.Services;

public class EmailService
{
    public async Task<bool> SendEmailAsync(string email, string name)
    {
        var connectionString = "endpoint=https://commucationserviceonatrix.europe.communication.azure.com/;accesskey=AYMQc2Ln6LJEpqYWMEH0kXwo5QFdszGZ8zKB0nsVTJQmY4PUKlh1JQQJ99AIACULyCpq7IbPAAAAAZCSHseq";
        var emailClient = new EmailClient(connectionString);
        var emailContent = new EmailContent("Thank you for contacting us")
        {
            PlainText = $"Hey! {name} Thank you for contacting us from {email} we will get back to you as soon as we can!",
            Html = $"<p>Hey! <strong>{name}</strong> Thank you for contacting us from {email} we will get back to you as soon as we can!</p>"
        };

        var emailMessage = new EmailMessage(
                senderAddress: "donotreply@4b9dd81e-98d4-4cc4-ad8e-4418460d41fe.azurecomm.net",
                content: emailContent,
                recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(email)})
            );
        try
        {
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
            return emailSendOperation.HasCompleted;
        }
        catch (Exception)
        {

            return false;
        }
    }
}
