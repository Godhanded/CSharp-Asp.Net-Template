namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface IPasswordService
    {
        (string passwordSalt, string passwordHash) GeneratePasswordSaltAndHash(string plainPassword);

        bool IsPasswordEqual(string plainPassowrd, string passwordSalt, string passwordHash);
    }
}
