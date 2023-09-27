using System;
using DatingAppApi.Data;
using DatingAppApi.Entities.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DatingAppApi.Repositories.UsersRepository
{
	public class UsersRepository : IUsersRepository
    {
        private readonly DataContext dbContext;
		public UsersRepository(DataContext context)
		{
            this.dbContext = context;
		}

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<AppUser?> GetUserById(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }
    }
}

