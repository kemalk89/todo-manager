using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Project;
using Infrastructure.Auth;
using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks;

namespace TaskManager
{
    public class AppDbContext : DbContext
    {
        private readonly AuditableEntitiesManager _auditableEntitiesManager;
        private readonly IIdentityService _identityService;

        public DbSet<Todo> Tasks { get; set; }
        public DbSet<TodoComment> TaskComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        private readonly string Host = "localhost";
        private readonly string DbName = "postgres";
        private readonly string DbUser = "postgres";
        private readonly string DbPw = "";

        public AppDbContext() {
            // this constructor is for the seeding process
        }

        public AppDbContext(
            AuditableEntitiesManager auditableEntitiesManager,
            IIdentityService identityService,
            DbContextOptions<AppDbContext> options) : base(options)
        {
            _auditableEntitiesManager = auditableEntitiesManager;
            _identityService = identityService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            builder.UseNpgsql($"Host={Host};Database={DbName};Username={DbUser};Password={DbPw}");

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            User currentUser = null;
            var username = _identityService.GetUsername();
            if (username != null)
            {
                currentUser = Users.Where(x => x.Username == username).Single();
            }

            _auditableEntitiesManager.ProcessBeforeSave(ChangeTracker, currentUser);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}