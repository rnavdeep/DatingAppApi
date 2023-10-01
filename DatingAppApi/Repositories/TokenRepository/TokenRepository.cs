using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatingAppApi.Data;
using DatingAppApi.Entities.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace DatingAppApi.Repositories.TokenRepository
{
	public class TokenRepository:ITokenRepository
	{
        private readonly DataContext dbContext;
        private readonly SymmetricSecurityKey symmetricSecurityKey;//stay on server and never go to client,same key to encrypt and decrypt(signin and verify), other type is assymetric when server encrypt and client decrypt something

        public TokenRepository(DataContext context, IConfiguration configuration)
        {
            this.dbContext = context;
            this.symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token"]));
        }

        public string CreateToken(AppUser appUser)
        {
            //claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, appUser.UserName)
            };
            //create credentials
            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            //build token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}

