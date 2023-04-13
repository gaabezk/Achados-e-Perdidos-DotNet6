using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<RefreshTokenModel> RefreshToken { get; set; } = null!;
}
