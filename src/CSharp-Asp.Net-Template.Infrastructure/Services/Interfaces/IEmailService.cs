using CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> RenderEmailTemplate<T>(string templateName, T model);
        void SendEmailAsync<T>(MailRequest mailRequest, T model);
    }
}
