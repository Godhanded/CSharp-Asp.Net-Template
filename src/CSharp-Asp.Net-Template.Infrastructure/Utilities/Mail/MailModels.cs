namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels
{
    public record WelcomeMailModel(string Email, string Name, DateTime SignUpDate);
    public record ResetPasswordRequestModel(string ResetUrl, int TokenExpiry);
}
