using System.ComponentModel.DataAnnotations;
using Infrastructure.Entity;

namespace TaskManager.Tasks
{
    public class TodoComment : AuditableEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        public Todo Todo { get; set; }
    }
}
