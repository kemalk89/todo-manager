using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaskManager.Shared
{
    public class DbContextHelper
    {
        internal static void HandleAuditableEntitiesBeforeSaving(ChangeTracker changeTracker)
        {
            var entries = changeTracker.Entries();
            var now = DateTime.Now;

            foreach (var entry in entries)
            {
                if (entry.Entity is AuditableEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
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
