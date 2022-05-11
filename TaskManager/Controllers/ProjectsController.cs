using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<string> GetProjects()
        {
            return new List<string>();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> SaveProject(ProjectDto dto)
        {
            var project = ProjectDto.Map(dto);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return ProjectDto.Map(project);
        }

    }
}
