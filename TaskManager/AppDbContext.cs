using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Auth;
using TaskManager.Shared;

namespace TaskManager
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly string Host = "localhost";
        private readonly string DbName = "postgres";
        private readonly string DbUser = "postgres";
        private readonly string DbPw = "";

        public AppDbContext()
        {
            // this constructor is for the seeding process
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            builder.UseNpgsql($"Host={Host};Database={DbName};Username={DbUser};Password={DbPw}");

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DbContextHelper.HandleAuditableEntitiesBeforeSaving(ChangeTracker);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}