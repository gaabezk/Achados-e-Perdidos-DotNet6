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
                ServerVersion.Parse("8.0.32-mysql")
            ));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPostRepository, PostRepository>();
        services.AddAutoMapper(typeof(Mappings));
        services.AddTransient<TokenService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPostService, PostService>();
        
        return services;
    }
    
}