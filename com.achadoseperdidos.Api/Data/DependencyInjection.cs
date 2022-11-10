using com.achadoseperdidos.Api.Mappings;
using com.achadoseperdidos.Api.Repositories;
using com.achadoseperdidos.Api.Repositories.Interfaces;
using com.achadoseperdidos.Api.Services;
using com.achadoseperdidos.Api.Services.Interfaces;
using com.achadoseperdidos.Api.Validations;
using Microsoft.EntityFrameworkCore;

namespace com.achadoseperdidos.Api.Data;

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
        services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); });
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