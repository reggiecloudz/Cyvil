using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
    }
}