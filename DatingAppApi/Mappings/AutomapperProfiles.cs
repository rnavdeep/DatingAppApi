using System;
using AutoMapper;
using DatingAppApi.Entities.Domain.Users;
using DatingAppApi.Entities.DTO.Users;

namespace DatingAppApi.Mappings
{
	public class AutomapperProfiles: Profile
	{
		public AutomapperProfiles()
		{
			CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, AppUserDtoIncoming>().ReverseMap();
            CreateMap<LoginUser, LoginUserDto>().ReverseMap();
            CreateMap<AppUser, LoginUser>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();


        }
    }
}

