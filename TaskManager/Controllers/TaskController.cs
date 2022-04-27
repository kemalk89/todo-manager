using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Auth;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.ROLE_ADMIN)]
        public List<Task> GetAdminTasks()
        {
            return new List<Task>();
        }

        [HttpGet]
        [Authorize]
        public List<Task> GetTasks()
        {
            Task t1 = new Task
            {
                title = "Title 1",
                description = "Description Task 1"
            };

            Task t2 = new Task
            {
                title = "Title 2",
                description = "Description Task 2"
            };

            List<Task> tasks = new List<Task>();
            tasks.Add(t1);
            tasks.Add(t2);

            return tasks;
        }
    }
}