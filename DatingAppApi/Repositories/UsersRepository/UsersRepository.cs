using System;
using System.Security.Cryptography;
using System.Text;
using DatingAppApi.Data;
using DatingAppApi.Entities.Domain.Users;
using DatingAppApi.Entities.DTO.Users;
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

        public async Task<AppUser> Register(AppUser appUser,string password)
        {
            var user = dbContext.Users.Where(user => user.UserName.Equals(appUser.UserName)).Count();
            if ( user == 0)
            {
                using var hmac = new HMACSHA512();
                if (appUser.UserName != null && password != null)
                {
                    appUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    appUser.PasswordSalt = hmac.Key;
                }
                dbContext.Users.Add(appUser);
                await dbContext.SaveChangesAsync();
                return appUser;
            }
            return null;
            
        }

        public async Task<AppUser> Login(LoginUser loginUser)
        {
            var user =await  dbContext.Users.Where(user => user.UserName.Equals(loginUser.UserName)).SingleOrDefaultAsync();

            if (user != null)
            {
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
                for(int i= 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        return null;
                    }
                }
                return user;
            }
            return null;
        }
    }
}

