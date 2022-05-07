using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaskManager.Shared
{
    // this is a test comment
    public interface AuditableEntitiesManager
    {
        public void ProcessBeforeSave(ChangeTracker changeTracker, User currentUser);
    }
}
