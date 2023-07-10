using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;
using API_REST.Infrastructure.Models;
using AutoMapper;

namespace API_REST.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserRequestDTO, User>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<User, UserRespondDTO>();
        }
    }
}
