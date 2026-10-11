using Microsoft.EntityFrameworkCore;
using Calculator.Models;

namespace Calculator.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options) { }

    public DbSet<Operation> Operations { get; set; }
}