namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos
{
    public class UserDto
    {
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasAccess { get; set; }
    }
}