﻿using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddDbContext<DataContext>(options =>
    {
      options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });

    return services;
  }
}
