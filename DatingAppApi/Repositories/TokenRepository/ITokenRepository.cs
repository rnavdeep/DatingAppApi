using System;
using DatingAppApi.Entities.Domain.Users;

namespace DatingAppApi.Repositories.TokenRepository
{
	public interface ITokenRepository
	{
		string CreateToken(AppUser appUser);
	}
}

