using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {


                      _context = context;
        }

    }
}
