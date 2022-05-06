using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Shared
{
    public abstract class AuditableEntity
    {
        [Required]
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public User UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
