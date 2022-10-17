using AutoMapper;
using XploraAPI.Security.Domain.Models;
using XploraAPI.Security.Domain.Services.Communication;
using XploraAPI.Security.Resources;

namespace XploraAPI.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}