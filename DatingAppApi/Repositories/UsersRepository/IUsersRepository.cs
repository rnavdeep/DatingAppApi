using System;
using DatingAppApi.Entities.Domain.Users;

namespace DatingAppApi.Repositories.UsersRepository
{
	public interface IUsersRepository
	{
		Task<List<AppUser>> GetAllAsync();
		Task<AppUser?> GetUserById(int id);
	}
}

