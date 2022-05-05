using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Auth;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context) 
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

        [HttpPost]
        public async Task<ActionResult<Todo>> SaveTask(Todo task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = task.Id }, task);
        }
    }
}