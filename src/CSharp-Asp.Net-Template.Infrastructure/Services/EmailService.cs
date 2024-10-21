using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorLight;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class EmailService(RazorLightEngine razorEngine, IOptions<MailOptions> mailOptions, ILogger<EmailService> logger) : IEmailService
    {
        private readonly RazorLightEngine _razorEngine = razorEngine;
        private readonly ILogger<EmailService> _logger = logger;
        private readonly MailOptions _mailOptions = mailOptions.Value;

        public async Task<string> RenderEmailTemplate<T>(string templateName, T model)
        {

            string templatePath = $"Templates.{templateName}.cshtml";
            string emailBody = await _razorEngine.CompileRenderAsync(templatePath, model);
            return emailBody;
        }

        public void SendEmailAsync<T>(MailRequest mailRequest, T model)
        {
            Task.Run(async () =>
            {
                try
                {
                    var email = new MimeMessage
                    {
                        Sender = MailboxAddress.Parse(_mailOptions.Mail),
                        Subject = mailRequest.Subject,
                    };
                    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));


                    var bodyBuilder = new BodyBuilder();
                    if (mailRequest.Attachments is not null)
                        ProcessAttachements(mailRequest, bodyBuilder);
                    bodyBuilder.HtmlBody = await RenderEmailTemplate(mailRequest.Template, model);

                    email.Body = bodyBuilder.ToMessageBody();

                    using var smtp = new SmtpClient();
                    await smtp.ConnectAsync(_mailOptions.Host, _mailOptions.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_mailOptions.Mail, _mailOptions.Password);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                };
            });
        }

        private void ProcessAttachements(MailRequest mailRequest, BodyBuilder bodyBuilder)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
    }
}
