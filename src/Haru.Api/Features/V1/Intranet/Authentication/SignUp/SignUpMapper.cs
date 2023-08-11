using AutoMapper;
using Haru.Api.Commons.Enums;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.V1.Intranet.Authentication.SignUp;

public class SignUpMapper : Profile
{
    public SignUpMapper()
    {
        CreateMap<SignUpCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(StatusEnum), StatusEnum.Active)));
    }
}