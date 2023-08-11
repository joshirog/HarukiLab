using AutoMapper;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.V1.Intranet.Authentication.SignIn;

public class SignInMapper : Profile
{
    public SignInMapper()
    {
        CreateMap<SignInCommand, User>();
    }
}