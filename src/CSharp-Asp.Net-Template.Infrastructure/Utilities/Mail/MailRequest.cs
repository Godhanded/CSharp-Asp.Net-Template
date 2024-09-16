using Microsoft.AspNetCore.Http;

namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels
{
    public class MailRequest
    {
        public MailRequest(string toEmail, string subject, string template, List<IFormFile> attachments = null)
        {
            ToEmail = toEmail;
            Subject = subject;
            Template = template;
            Attachments = attachments;
        }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
