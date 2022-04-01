using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext 
{  

    public DbSet<Produto> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Produto>()
            .Property(p => p.Description).HasMaxLength(500).IsRequired(false);
        builder.Entity<Produto>()
            .Property(p => p.Name).HasMaxLength(120).IsRequired();
        builder.Entity<Produto>()
            .Property(p => p.Code).HasMaxLength(20).IsRequired();
        builder.Entity<Category>()
            .ToTable("Categories");
    } 

}
