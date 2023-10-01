using System;
namespace DatingAppApi.Entities.Domain.Users
{
	public class AppUser
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }
	}
	public class LoginUser
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

