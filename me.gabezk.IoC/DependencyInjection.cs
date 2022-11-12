using me.gabezk.Application.Mappings;
using me.gabezk.Application.Services;
using me.gabezk.Application.Services.Interfaces;
using me.gabezk.Data.Context;
using me.gabezk.Data.Repositories;
using me.gabezk.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace me.gabezk.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.Parse("8.0.30-mysql")
            ));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DomainToDtoMapping));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        return services;
    }
}