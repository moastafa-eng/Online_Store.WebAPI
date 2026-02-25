using AutoMapper;
using Domain.Entities.Identity;
using Shard.DTOs.Auth;

namespace Services.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
