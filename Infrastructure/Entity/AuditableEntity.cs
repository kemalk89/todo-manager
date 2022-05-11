using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Auth;

namespace Infrastructure.Entity
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
