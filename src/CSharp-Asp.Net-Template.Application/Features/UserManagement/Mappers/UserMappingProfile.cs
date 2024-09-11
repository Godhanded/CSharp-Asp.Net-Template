using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Domain.Entities;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ReverseMap();

            CreateMap<UserRegisterDto, User>()
                .ReverseMap();
        }
    }
}
