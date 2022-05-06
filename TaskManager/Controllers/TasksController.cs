using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Auth;
using TaskManager.Tasks;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private AppDbContext _context;

        public TasksController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet("admin/tasks")]
        [Authorize(Roles = UserRoles.ROLE_ADMIN)]
        public List<Todo> GetAdminTasks()
        {
            if (_context.Database.CanConnect()) {
                return null;
            } else {
                return new List<Todo>();
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTasks()
        {
            var tasks = await _context.Tasks.Select(x => x).ToListAsync();
            return tasks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> SaveTask(TodoDTO dto)
        {
            var task = TodoDTO.ToTodo(dto);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoDTO dto)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Update(dto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!IsExistingTodo(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IsExistingTodo(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}