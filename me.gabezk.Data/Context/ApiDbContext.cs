using me.gabezk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace me.gabezk.Data.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Post> Post { get; set; }
}