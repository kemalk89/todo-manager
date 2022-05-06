using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Shared;
using TaskManager.Tasks;

namespace TaskManager
{
    public class Todo : AuditableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public void Update(TodoDTO dto)
        {
            Description = dto.Description;
            Title = dto.Title;
        }
    }
}
