namespace todolist.Database;

#nullable disable

public class TodoListDataContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    public TodoListDataContext(DbContextOptions<TodoListDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.Entity<UserEntity>(item =>
        {
            item.HasIndex(e => e.Email).IsUnique();
            item.HasIndex(e => e.Username).IsUnique();
        });
    }
}
