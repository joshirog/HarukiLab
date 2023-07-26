using AutoMapper;
using Haru.Api.Domains.Entities;

namespace Haru.Api.Features.v1.Authentication.SignIn;

public class SignInMapper : Profile
{
    public SignInMapper()
    {
        CreateMap<SignInCommand, User>();
    }
}