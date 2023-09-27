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
		}
	}
}

