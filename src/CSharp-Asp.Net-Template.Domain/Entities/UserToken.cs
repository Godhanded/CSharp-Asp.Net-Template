namespace CSharp_Asp.Net_Template.Domain.Entities
{
    public class UserToken : EntityBase
    {
        public Guid UserId { get; set; }
        public string? Token { get; set; } // Hashed token
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; } = false;
        public User User { get; set; }
    }
}
