using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Entity;
using TaskManager.Tasks;

namespace TaskManager
{
    public class Todo : AuditableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public List<TodoComment> Comments { get; set; } = new List<TodoComment>();

        public void Update(TodoDTO dto)
        {
            Description = dto.Description;
            Title = dto.Title;
        }
    }
}
