using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Auth;

namespace TaskManager
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly ILogger<ApplicationDbContext> _logger;

        private readonly string Host = "localhost";
        private readonly string DbName = "postgres";
        private readonly string DbUser = "postgres";
        private readonly string DbPw = "";

        public ApplicationDbContext(
            ILogger<ApplicationDbContext> logger,
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            builder.UseNpgsql($"Host={Host};Database={DbName};Username={DbUser};Password={DbPw}");

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            _logger.LogInformation("Seeding admin user");

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "password",
                Role = UserRoles.ROLE_ADMIN
            });

            _logger.LogInformation("Seeding task");

            modelBuilder.Entity<Todo>().HasData(new Todo
            {
                Id = 1,
                Title = "Dummy Task Title",
                Description = "Dummy Task Description"
            });
        }
    }
}
