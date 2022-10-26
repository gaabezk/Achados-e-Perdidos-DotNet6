using com.achadoseperdidos.Api.Mappings;
using com.achadoseperdidos.Api.Repositories;
using com.achadoseperdidos.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace com.achadoseperdidos.Api.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
//        services.AddScoped<IProdutoRepository, ProdutoRepository>();
//        services.AddScoped<ICompraRepository, CompraRepository>();
//        services.AddScoped<IUnityOfWork, UnityOfWork>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DomainToDtoMapping));
        services.AddScoped<IUserService, UserService>();
//        services.AddScoped<IProdutoService, ProdutoService>();
//        services.AddScoped<ICompraService, CompraService>();
        return services;
    }
}