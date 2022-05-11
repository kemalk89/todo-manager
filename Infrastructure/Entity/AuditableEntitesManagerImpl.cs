using System;
using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Entity
{
    public class AuditableEntitesManagerImpl : AuditableEntitiesManager
    {
        public void ProcessBeforeSave(ChangeTracker changeTracker, User currentUser)
        {
            var entries = changeTracker.Entries();
            var now = DateTime.Now;

            foreach (var entry in entries)
            {
                if (entry.Entity is AuditableEntity auditable)
                {

                    if (entry.State == EntityState.Added)
                    {
                        if (currentUser != null)
                        {
                            auditable.CreatedBy = currentUser;
                        }
                        auditable.CreatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedAt = now;
                    }
                }
            }
        }
    }
}
