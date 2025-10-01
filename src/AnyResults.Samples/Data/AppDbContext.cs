using AnyResults.Samples.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnyResults.Samples.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoEntity> TodoList { get; set; } = default!;
}