using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Auth;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<Todo>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.CreatedBy) // eager loading of CreatedBy
                .ToListAsync();
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

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<TodoCommentDTO>> SaveTaskComment(int id, TodoCommentDTO dto)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var comment = dto.ToTodoComment();
            todo.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var routeValues = new
            {
                id,
                commentId = comment.Id
            };
            return CreatedAtAction(nameof(GetTodoComment), routeValues, TodoCommentDTO.Create(comment));
        }

        [HttpDelete("{id}/comments/{commentId}")]
        public async Task<IActionResult> DeleteTaskComment(int id, int commentId)
        {
            //var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var moo = User.Identity;
            //var moo = System.Security.Principal.WindowsIdentity.GetCurrent();
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var comment = todo.Comments.Select(x => x).ToList().Find(x => x.Id == commentId);
            //var comment = todo.Comments.Find(x => x.Id == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            todo.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/comments/{commentId}")]
        public async Task<ActionResult<TodoComment>> GetTodoComment(int id, int commentId)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var comment = todo.Comments.Find(x => x.Id == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpGet("{id}/comments")]
        public async Task<ActionResult<IEnumerable<TodoComment>>> GetTodoComments(int id)
        {
            var todo = await _context.Tasks.FindAsync(id);
            if (todo == null)
            {
                return new List<TodoComment>();
            }

            return todo.Comments;
        }

        private bool IsExistingTodo(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}