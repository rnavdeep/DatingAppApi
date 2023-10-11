using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingAppApi.Data;
using DatingAppApi.Entities.Domain.Users;
using DatingAppApi.Entities.DTO.Users;
using DatingAppApi.Repositories.TokenRepository;
using DatingAppApi.Repositories.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatingAppApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IMapper mapper;
        public UsersController(IUsersRepository usersRepository,IMapper mapper, ITokenRepository tokenRepository)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            this.tokenRepository = tokenRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await usersRepository.GetAllAsync();
            return (users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await usersRepository.GetUserById(id);
            if (user != null)
            {
                return Ok(user);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] AppUserDtoIncoming appUserDtoIncoming)
        {
            if (ModelState.IsValid)
            {
                //convert dto to domain model
                var appuser = mapper.Map<AppUser>(appUserDtoIncoming);

                appuser = await usersRepository.Register(appuser, appUserDtoIncoming.Password);

                if (appuser != null)
                {
                    //convert to dto
                    var returnAppUser = new UserDto
                    {
                        UserName = appuser.UserName,
                        Token = tokenRepository.CreateToken(appuser)
                    };

                    return returnAppUser;
                }
                else
                {
                    return BadRequest("Username already exists");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var loginUser = mapper.Map<LoginUser>(loginUserDto);
            var appUser = await usersRepository.Login(loginUser);
            if(appUser != null)
            {
                var returnAppUser = new UserDto
                {
                    UserName = appUser.UserName,
                    Token = tokenRepository.CreateToken(appUser)
                };
                return returnAppUser ;
            }
            else
            {
                throw new Exception("Invalid UserName or Password");
            }
                                 
        }
    }
}

