using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Entity
{
    // this is a test comment!!!
    public interface AuditableEntitiesManager
    {
        public void ProcessBeforeSave(ChangeTracker changeTracker, User currentUser);
    }
}
