using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<TaskItem> Tasks { get; set; }
    
    public DbSet<BugTask> Bugs { get; set; }

    public DbSet<FeatureTask> Features { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tasks.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>().ToTable("Tasks");
        modelBuilder.Entity<BugTask>().ToTable("BugTasks");
        modelBuilder.Entity<FeatureTask>().ToTable("FeatureTasks");

        modelBuilder.Entity<TaskItem>().Property(t=>t.Status).HasConversion<string>();
        modelBuilder.Entity<TaskItem>().Property(t=>t.Type).HasConversion<string>();
        modelBuilder.Entity<BugTask>().Property(t=>t.Severity).HasConversion<string>();
        modelBuilder.Entity<FeatureTask>().Property(t=>t.Priority).HasConversion<string>();

    }

}