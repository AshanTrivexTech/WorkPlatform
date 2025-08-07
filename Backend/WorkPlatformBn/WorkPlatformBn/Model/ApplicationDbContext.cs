using Microsoft.EntityFrameworkCore;

namespace WorkPlatformBn.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<WorkerCategory> WorkerCategories { get; set; }

}