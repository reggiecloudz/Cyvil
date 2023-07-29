using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatesController> _logger;

        public StatesController(ILogger<StatesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("{stateId}/[action]")]
        public async Task<JsonResult> GetCities(long stateId)
        {
            var cities = await _context.Cities.Where(c => c.StateId == stateId).OrderBy(c => c.Name).ToListAsync();
            return new JsonResult(cities);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}