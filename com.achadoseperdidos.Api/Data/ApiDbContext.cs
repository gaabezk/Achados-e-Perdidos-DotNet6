using com.achadoseperdidos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace com.achadoseperdidos.Api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<Post> Post { get; set; }
    
}