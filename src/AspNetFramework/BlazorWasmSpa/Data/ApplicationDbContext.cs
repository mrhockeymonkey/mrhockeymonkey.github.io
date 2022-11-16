using Microsoft.EntityFrameworkCore;

namespace BlazorWasmSpa.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<PlantEntity> Plants { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlantEntity>()
            .HasKey(p => p.PlantId);

        modelBuilder.Entity<PlantEntity>()
            .HasIndex(p => p.LastWatered);

        modelBuilder.Entity<PlantEntity>().HasData(new PlantEntity[]
        {
            new PlantEntity
            {
                PlantId = 1,
                PlantName = "foo",
                PlantHeightCm = 25d,
                LastWatered = DateTimeOffset.Now
            }
        });
    }
    
}