using System;
using DatingAppApi.Data;
using DatingAppApi.Mappings;
using DatingAppApi.Repositories.TokenRepository;
using DatingAppApi.Repositories.UsersRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatingAppApi.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services,
			IConfiguration configuration)
		{
            //inject db service to the application
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(AutomapperProfiles));
            services.AddCors();


            //inject services  
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;




        }
    }
}

