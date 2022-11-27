using Microsoft.EntityFrameworkCore;

namespace RESTful.API.Data;
public sealed class RestfulDbContext : DbContext
{
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    public RestfulDbContext(DbContextOptions<RestfulDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntity>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
