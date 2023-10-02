using System;
using DatingAppApi.Entities.Domain.Users;
using DatingAppApi.Entities.DTO.Users;

namespace DatingAppApi.Repositories.UsersRepository
{
	public interface IUsersRepository
	{
		Task<List<AppUser>> GetAllAsync();
		Task<AppUser?> GetUserById(int id);
		Task<AppUser> Register(AppUser appUser,string password);
        Task<AppUser> Login(LoginUser loginUser);

    }
}

