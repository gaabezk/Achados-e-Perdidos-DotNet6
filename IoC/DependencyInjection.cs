using Application.Mappings;
using Application.Services;
using Application.Services.Interfaces;
using Data.Context;
using Data.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyInjection
{   
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MySqlContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
                ServerVersion.Parse("8.0.30-mysql")
            ));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddAutoMapper(typeof(Mappings));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        
        return services;
    }
    
}