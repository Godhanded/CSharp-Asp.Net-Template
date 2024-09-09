using CSharp_Asp.Net_Template.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
