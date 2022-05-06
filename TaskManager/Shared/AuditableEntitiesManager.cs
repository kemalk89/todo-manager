using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaskManager.Shared
{
    public interface AuditableEntitiesManager
    {
        public void ProcessBeforeSave(ChangeTracker changeTracker, User currentUser);
    }
}
