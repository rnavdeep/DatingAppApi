using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DatingAppApi.Entities.DTO.Users
{
	public class AppUserDto
	{
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
    
    public class AppUserDtoIncoming
    {
        private static readonly string passwordPattern1 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*!])[A-Za-z\d@#$%^&*!]{8,}$";
        Regex regex = new Regex(passwordPattern1);
        [Required]
        [MinLength(3, ErrorMessage ="Minimum length of is 3 characters.")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*!])[A-Za-z\d@#$%^&*!]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

    }
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class UserDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}

